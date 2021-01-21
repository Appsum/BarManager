using MediatR;

namespace Bartender.Drinks.Application.Queries
{
    public interface IQuery<out T> : IRequest<T> { }
}