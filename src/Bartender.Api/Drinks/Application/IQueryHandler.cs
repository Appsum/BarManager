using Bartender.Api.Drinks.Application.Queries;

using MediatR;

namespace Bartender.Api.Drinks.Application
{
    public interface IQueryHandler<in TQuery, TResult>
        : IRequestHandler<TQuery, TResult> where TQuery : IQuery<TResult> { }
}