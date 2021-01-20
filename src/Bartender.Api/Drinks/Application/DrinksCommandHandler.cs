using System.Threading;
using System.Threading.Tasks;

using Bartender.Api.Drinks.Application.Commands;
using Bartender.Api.Drinks.Domain;
using Bartender.Api.Drinks.Domain.Repositories;

using MediatR;

namespace Bartender.Api.Drinks.Application
{
    public class DrinksCommandHandler : ICommandHandler<AddDrink>, ICommandHandler<RenameDrink>, ICommandHandler<DeleteDrink>, ICommandHandler<OrderDrinks>
    {
        private readonly IDrinksRepository _drinksRepository;

        public DrinksCommandHandler(IDrinksRepository drinksRepository)
        {
            _drinksRepository = drinksRepository;
        }

        public async Task<Unit> Handle(AddDrink request, CancellationToken cancellationToken)
        {
            await _drinksRepository.Add(new Drink(request.Name));
            return Unit.Value;
        }

        public async Task<Unit> Handle(RenameDrink request, CancellationToken cancellationToken) => throw new System.NotImplementedException();

        public async Task<Unit> Handle(DeleteDrink request, CancellationToken cancellationToken) => throw new System.NotImplementedException();

        public async Task<Unit> Handle(OrderDrinks request, CancellationToken cancellationToken) => throw new System.NotImplementedException();
    }
}