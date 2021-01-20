using MediatR;

namespace Bartender.Api.Drinks.Application.Commands
{
    public interface ICommand<out T> : IRequest<T> { }
}