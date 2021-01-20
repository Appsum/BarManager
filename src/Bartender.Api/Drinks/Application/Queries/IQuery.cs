using MediatR;

namespace Bartender.Api.Drinks.Application.Queries
{
    public interface IQuery<out T> : IRequest<T> { }
}