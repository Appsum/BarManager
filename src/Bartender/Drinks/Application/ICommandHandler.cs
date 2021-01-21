using Bartender.Drinks.Application.Commands;

using MediatR;

namespace Bartender.Drinks.Application
{
    public interface ICommandHandler<in TCommand> 
        : IRequestHandler<TCommand> where TCommand : ICommand { }
}