using System.Threading;
using System.Threading.Tasks;

using Bartender.Drinks.Application.Commands;
using Bartender.Drinks.Domain;
using Bartender.Drinks.Domain.Repositories;

using MediatR;

namespace Bartender.Drinks.Application
{
    public class DrinksCommandHandler : ICommandHandler<CreateDrink>, ICommandHandler<RenameDrink>, ICommandHandler<DeleteDrink>, ICommandHandler<OrderDrinks>
    {
        private readonly IDrinksRepository _drinksRepository;

        public DrinksCommandHandler(IDrinksRepository drinksRepository)
        {
            _drinksRepository = drinksRepository;
        }

        public async Task<Unit> Handle(CreateDrink request, CancellationToken cancellationToken)
        {
            await _drinksRepository.Add(new Drink(request.Name));
            return Unit.Value;
        }

        public async Task<Unit> Handle(RenameDrink request, CancellationToken cancellationToken)
        {
            Drink drink = await _drinksRepository.GetById(request.DrinkId);

            drink.Rename(request.NewName);
            await _drinksRepository.Update(request.DrinkId, drink);

            return Unit.Value;
        }

        public async Task<Unit> Handle(DeleteDrink request, CancellationToken cancellationToken)
        {
            await _drinksRepository.Delete(request.DrinkId);
            return Unit.Value;
        }

        public Task<Unit> Handle(OrderDrinks request, CancellationToken cancellationToken)
        {
            // To be implemented with EventBus

            return Task.FromResult(Unit.Value);
        }
    }
}