# Table of contents

1. [The BarManager application](#the-barmanager-application)
2. [Basic version of Clean Architecture](#basic-version-of-clean-architecture)
3. [MediatR](#mediatr)
4. [Azure Table Storage](#azure-table-storage)
5. [Azure Service Bus](#azure-service-bus)

-   [Configure the publishing in the Bartender project](#configure-the-publishing-in-the-bartender-project)
-   [Configure the subscription in the Waitress project](#configure-the-subscription-in-the-waitress-project)

6. [SignalR server setup](#signalr-server-setup)
7. [Azure Blob Storage](#azure-blob-storage)
8. [Dockerfile creation](#dockerfile-creation)

# The BarManager application

The **Barterder project** exposes an Api to get, create, rename, delete and order drinks. The storage that is used will be Azure Table Storage. Ordering drinks puts a message with contract OrderPlaced on the Azure Servicebus.

The **Waitress.Api project** listens to the same Azure ServiceBus topic for an OrderPlaced message, which puts a message on the SignalR 'TablesHub' for clients to subscribe on. It also creates a receipt file on the Azure Blob storage which contains the data of the order.

The **Tables project** is a demo client for the SignalR Hub created in [SignalR server setup](#signalr-server-setup).

## Basic version of Clean Architecture

### **Drinks feature**

### Domain

-   Entities, currently only Drink
-   Interfaces for the repositories for the entities
-   DomainEvents when an entity is changes (not used in this sample app)

### Application

-   CQRS
    -   Commands: do actions on the entities
    -   Queries: query for entities and their data
    -   Handlers: contains the handle methods for the commands and queries when they are sent to the Mediator
-   Defines and uses the interfaces to cross cutting concerns like database, eventbus, ...

### Infrastructure

-   Implementations of the cross cutting concerns like which database, which type of eventbus, ...

### Api

-   Controllers
    -   These will send the command/query request to the IMediator
-   Dto's for the actions
    -   FluentValidation on dto's (configured in Startup using

```csharp
services.AddControllers()
  .AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssemblyContaining<Startup>()))
```

## MediatR

Install Nuget package
`MediatR.Extensions.Microsoft.DependencyInjection`
Uncomment

```
services.AddMediatR(typeof(Startup));
```

Make the Commands and queries implement IRequest<>

MediatR has no distinction between Commands and queries, everything is a Request. This distinction can be created with an interface for each:

```csharp
public interface ICommand : IRequest<Unit> { }
```

```csharp
public interface IQuery<out T> : IRequest<T> { }
```

And for the handlers:

```csharp
public interface ICommandHandler<in TCommand>
    : IRequestHandler<TCommand> where TCommand : ICommand { }
```

```csharp
public interface IQueryHandler<in TQuery, TResult>
    : IRequestHandler<TQuery, TResult> where TQuery : IQuery<TResult> { }
```

Events inherit from `INotification<>`

### **Implement the Command & Query Handlers**

See branch `1_implement_mediator` for both implemented handlers
The OrderDrink command is currently not being handled, because this will use the EventBus in a later stage

### **Use the IMediator in the controllers**

The IMediator will be used in the controller to dispatch the commands and queries to their registered handlers (which is done by convention through DI)

-   Using the `Send()` method, a request/response pattern is used
    -   This means that only one handler can handle the request and a response is expected (Unit, an empty response, is also a response)
-   Using the `Publish()` method, a pub/sub or publish/subscribe pattern is used
    -   This means that multiple Handlers can listen to this and no response is expected

## Azure Table Storage

In Azure Portal:

1. Create resource group with name
2. Create Storage Account in the resource group
3. In the Storage Account, under Table service, create a new Table called `Drinks`

In the Bartender project, add the required NuGet package: `Microsoft.Azure.Cosmos.Table`
Set the ConnectionString and TableName in the `appsettings.json` file

### **Set up Azure Table Storage in the domain**

> **Important for DDD/Clean Architecture**: Azure Table Storage is not the best way to store entities, because it requires a mutable entity (public parameterless ctor, public setters). This application is just a demo for the Azure services. You should keep the domain model agnostic of any implementation details like what storage is used.

Make the Drink entity implement `TableEntity`, add a public parameterless ctor and public setters to all properties
Make the Id a get-only to the RowKey property, which Table Storage uses internally.
Set the PartitionKey to the name of the Entity class.
See the `2_table-storage` branch for the inplementation of the new Drink class

Register the CloudTableClient in DI in the Startup class in ConfigureServices:

```csharp
services.AddTransient(provider =>
{
    var options = provider.GetService<IOptions<TableStorageSettings>>();
    if (options == null)
    {
        throw new NullReferenceException("Table Storage Settings are not set in the configuration");
    }

    CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(options.Value.ConnectionString);
    return cloudStorageAccount.CreateCloudTableClient();
});
```

Implement the `AzureTableStorageRepository` using the `CloudTableClient` through DI
See the `2_table-storage` branch for the inplementation

Register the new `AzureTableStorageRepository` in DI as implementation of `IDrinksRepository`

```csharp
// services.AddSingleton<IDrinksRepository, InMemoryDrinksRepository>();
services.AddTransient<IDrinksRepository, AzureTableStorageDrinksRepository>();
```

## Azure Service Bus

In the Azure Portal:

1. Create a Service Bus (Integration > Service Bus) in the same resource group
    - Make sure to select Standard or Premium price tier for Topic support
2. Under Entities > Topics, create a Topic called drinkorders
3. On the Topic, create a Subscription called waitress with a max delivery count of 10

In the Bartender and waitress project, install the NuGet package `Microsoft.Azure.ServiceBus`
Get the connection string from the Service Bus Namespace > Settings > Shared access policies > RootManagedSharedAccessKey
Set the ConnectionString and TopicName in the `appsettings.json` file in the Bartender project
Set the Connectionstring, TopicName and SubscriptionName in the file in the Waitress project

### **Configure the publishing in the Bartender project**

Register a new `TopicClient` with the settings as an ITopicClient:

```csharp
services.AddSingleton<ITopicClient>(serviceProvider =>
{
    ServiceBusSettings serviceBusSettings = serviceProvider.GetRequiredService<IOptions<ServiceBusSettings>>().Value;
    return new TopicClient(serviceBusSettings.ConnectionString, serviceBusSettings.TopicName, RetryPolicy.Default);
});
```

In the Infrastructure layer, add a concrete implementation of the `IEventBus` as `AzureEventBus` that uses the `TopicClient` to publish to the Topic with the message as a serialized JSON string
See the implementation in the `3_service-bus` branch

Register the new `AzureEventBus` as the `IEventBus`:

```csharp
// services.AddTransient<IEventBus, NullEventBus>();
services.AddTransient<IEventBus, AzureEventBus>();
```

Use the `IEventBus` in the `DrinksCommandHandler` to publish an `OrderPlaced` message on the ServiceBus when an `OrderDrinks` command is sent through the Mediator.

The Bartender will now put a message on the ServiceBus when an order is placed.

### **Configure the subscription in the Waitress project**

Add a BusListener BackgroundService, which will register to the Subscription on the ServiceBus to listen when a message arrives. When it arrives, publish an `OrderPlacedMessageReceived` event through the Mediator which is already configured on the Waitress project.
This will trigger all subscribed handlers to do their action when it is published. In MediatR, the subscribed handlers implement `INotificationHandler<TEventType>`.
See the Handlers folder in the `3_service-bus` branch.
For the background listener implementation, see the `BusListener.cs` class.

Register the `BusListener` as [ASP.NET Core HostedService](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/host/hosted-services) in the `Startup.cs`:

```csharp
services.AddHostedService<BusListener>();
```

Now, when a message is received from the ServiceBus, the `NotificationHandler`'s `Handle` method will be called.

## SignalR server setup

The `TablesNotificationHandler` will be used to signal client connected to the Tables SignalR Hub that a new Order will be delivered. To set this up, add SignalR in the Waitress project's `Startup.cs` which is included in the `Microsoft.AspNetCore.App` shared framework.

```csharp
services.AddSignalR();
```

> To enable scaling horizontal of a SignalR Server (multiple instances/replicas), a **backplane** should be used to share the state between them. This can be an **Azure SignalR service** or a Redis database.
> To add the Azure SignalR service as a backplane, create the service in Azure and add `AddAzureSignalR` to the SignalR registration
>
> ```csharp
> services.AddSignalR().AddAzureSignalR(Configuration["SignalRConnectionString"])
> ```
>
> For this demo, we will skip adding a backplane.

Create a TablesHub inheriting from the `Hub` base class

Register an endpoint "/tables" for the new Tables Hub in the `Startup.cs`:

```csharp
endpoints.MapHub<TablesHub>("tables");
```

Setup the `TablesNotificationHandler` to send the OrderedDrinks from the notification to all clients listening to an invocation of the `Deliver` method on the `TablesHub` using the injectable `IHubContext<TablesHub>` ([more info](https://docs.microsoft.com/en-us/aspnet/core/signalr/hubcontext)). The method does not have to exist on the TablesHub, because it will be sent directly to the clients.

See the `4_signalr-server` branch for the implementation.
Start the Tables project together with both Api projects to test this functionality.

## Azure Blob Storage

Azure Blob storage is used as file storage for any type of file.

1. In the Azure Portal, go to the previously created Storage account
2. Under Blob service > Container, create a new Blob container called 'receipts' with private access level
3. Get the Storage account ConnectionString from Settings > Access keys

Put the ConnectionString appsettings.json of the `Waitress project` under Azure:BlobStorage. Set the ContainerName to 'receipts'

Add the NuGet packages to the `Waitress project`: `Azure.Storage.Blobs` (**important**, get v12.7.0, 12.8.0 has a bug) and `Microsoft.Extensions.Azure`

Register the BlobServiceClient in an AzureClients builder in the `Startup.cs`:

```csharp
services.AddAzureClients(builder =>
{
    builder.AddBlobServiceClient(Configuration[$"{BlobStorageSettings.ConfigurationKey}:ConnectionString"]);
});
```

Use the `BlobServiceClient` and the ContainerName from the `BlobStorageSettings` to upload a receipt to the Blob Container in the `ReceiptNotificationHandler`.
The Stream required to upload is found on the `OrderPlacedMessageReceived` as `.AsCsvStringStreamForBlobStorage()`.
See implementation in `5_blob-storage`

## Dockerfile creation

### **Docker**

Add Docker support to both the API projects, using Linux containers (these are cheaper as VMs on AKS later on)

This creates the Dockerfiles. Visual Studio will build the images as well if Docker Desktop is running.

-   You can list the installed images by using `docker images`
-   To run a container using the docker image, use `docker run <image-name>` (add -d to have the console back after starting the docker container)
-   See the running containers using `docker ps` (add -a to also see stopped containers)
-   Stop a container using `docker stop`
-   See help of all commands using `docker` or adding `--help` after the command you need help for

> If the application will be deployed to a webserver with a reverse proxy in front that will handle the HTTPS redirection, remove this from the Startup.cs [(more info)](https://docs.microsoft.com/en-us/aspnet/core/security/enforcing-ssl?view=aspnetcore-5.0&tabs=visual-studio#require-https):
>
> ```csharp
> // app.UseHttpsRedirection();
> ```

### **Docker Compose**

Docker compose can start multiple containers at once.
Add support for Container orchestration on one of projects.

> If the project has HTTPS support, you need to create a self-signed dev certificate that will be added to the container.
> Remove the existing dev certificates:
>
> ```powershell
> dotnet dev-certs https --check --verbose
> dotnet dev-certs https --clean
> dotnet dev-certs https --check --verbose
> ```
>
> Create a new certificate using the command `dotnet dev-certs https -ep $env:appdata/ASP.NET/Https/dev.pfx -p password`
> Trust the new certificate on your system to have it used for all HTTPS projects in .NET Core and 5 `dotnet dev-certs https --trust`
> In the `docker-compose.override.yml`, add the configuration for the new certificate to the environment variables of the projects:
>
> ```yaml
> environment:
>     - ASPNETCORE_HTTPS_PORT=5011
>     - ASPNETCORE_Kestrel__Certificates__Default__Password=password
>     - ASPNETCORE_Kestrel__Certificates__Default__Path=/root/.aspnet/https/dev.pfx
> ```
>
> The volumes to the `/root/.aspnet/https` container path were added by Visual Studio

Add the ports mapping for the exposed ports for each project in the `docker-compose.override.yml` file:

```yaml
ports:
    - 5000:80 # The container exposes port 80, map it to 5000
    - 5001:443 # The container exposes port 443, map it to 5001
```

-   Start the docker compose using `docker-compose up` (add -d to have the console back after starting)
-   Logging can be viewed using `docker-compose logs` (add -f for watch mode)
-   Stop the containers using `docker-compose down`
