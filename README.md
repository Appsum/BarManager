# BarManager
The **Barterder project** exposes an Api to get, create, rename, delete and order drinks. The storage that is used will be Azure Table Storage. Ordering drinks puts a message on the Azure Servicebus.

The **Waitress.Api project** listens to the same Azure ServiceBus topic for an OrderPlaced message, which puts a message on the SignalR Tables Hub for clients to subscribe on. It also creates a receipt file on the Azure Blob storage which contains the data of the order.
## Basic version of Clean Architecture
### Drinks feature

### Domain
- Entities, currently only Drink
- Interfaces for the repositories for the entities
- Events that can happen

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
 -FluentValidation on dto's (configured in Startup using
```csharp
services.AddControllers()
  .AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssemblyContaining<Startup>()))
```
