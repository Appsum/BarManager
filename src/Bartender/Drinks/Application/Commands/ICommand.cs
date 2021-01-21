using MediatR;

namespace Bartender.Drinks.Application.Commands
{
    public interface ICommand : IRequest<Unit> { }
}