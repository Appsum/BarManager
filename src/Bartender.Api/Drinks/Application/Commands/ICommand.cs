using MediatR;

namespace Bartender.Api.Drinks.Application.Commands
{
    public interface ICommand : IRequest<Unit> { }
}