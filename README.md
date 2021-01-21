# Table of contents
1. [The BarManager application](#the-barmanager-application)
2. [Basic version of Clean Architecture](#basic-version-of-clean-architecture)
3. [MediatR](#mediatr)

# The BarManager application
The **Barterder project** exposes an Api to get, create, rename, delete and order drinks. The storage that is used will be Azure Table Storage. Ordering drinks puts a message with contract OrderPlaced on the Azure Servicebus.

The **Waitress.Api project** listens to the same Azure ServiceBus topic for an OrderPlaced message, which puts a message on the SignalR Tables Hub for clients to subscribe on. It also creates a receipt file on the Azure Blob storage which contains the data of the order.

The **IntegrationEvents project** contains the shared contracts of the messages sent between the microservices.


## Basic version of Clean Architecture
### Drinks feature

### Domain
- Entities, currently only Drink
- Interfaces for the repositories for the entities
- DomainEvents when an entity is changes (not used in this sample app)

### Application
- CQRS
  - Commands: do actions on the entities
  - Queries: query for entities and their data
  - Handlers: contains the handle methods for the commands and queries when they are sent to the Mediator
- Defines and uses the interfaces to cross cutting concerns like database, eventbus, ...
### Infrastructure
- Implementations of the cross cutting concerns like which database, which type of eventbus, ...
### Api
- Controllers
  - These will send the command/query request to the IMediator
- Dto's for the actions
  - FluentValidation on dto's (configured in Startup using
```csharp
services.AddControllers()
  .AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssemblyContaining<Startup>()))
```

## MediatR
Install Nuget package
```MediatR.Extensions.Microsoft.DependencyInjection```
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
Events inherit from ```INotification<>```

### Implement the Command & Query Handlers
See branch ```1_implement_mediator``` for both implemented handlers
The OrderDrink command is currently not being handled, because this will use the EventBus in a later stage

### Use the IMediator in the controllers
The IMediator will be used in the controller to dispatch the commands and queries to their registered handlers (which is done by convention through DI)
- Using the ```Send()``` method, a request/response pattern is used
  - This means that only one handler can handle the request and a response is expected (Unit, an empty response, is also a response)
- Using the ```Publish()``` method, a pub/sub or publish/subscribe pattern is used
  - This means that multiple Handlers can listen to this and no response is expected
