using Bartender.Api.Drinks.Application.Commands;

using MediatR;

namespace Bartender.Api.Drinks.Application
{
    public interface ICommandHandler<in TCommand> 
        : IRequestHandler<TCommand> where TCommand : ICommand { }
}