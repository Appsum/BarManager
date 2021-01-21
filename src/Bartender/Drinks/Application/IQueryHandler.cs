using Bartender.Drinks.Application.Queries;

using MediatR;

namespace Bartender.Drinks.Application
{
    public interface IQueryHandler<in TQuery, TResult>
        : IRequestHandler<TQuery, TResult> where TQuery : IQuery<TResult> { }
}