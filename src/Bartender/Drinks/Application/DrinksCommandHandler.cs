using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Bartender.Drinks.Application.Commands;
using Bartender.Drinks.Application.Commands.Dtos;
using Bartender.Drinks.Application.EventBus;
using Bartender.Drinks.Domain;
using Bartender.Drinks.Domain.Events;
using Bartender.Drinks.Domain.Repositories;

using MediatR;

namespace Bartender.Drinks.Application
{
    public class DrinksCommandHandler : ICommandHandler<CreateDrink>, ICommandHandler<RenameDrink>, ICommandHandler<DeleteDrink>, ICommandHandler<OrderDrinks>
    {
        private readonly IDrinksRepository _drinksRepository;
        private readonly IEventBus _eventBus;

        public DrinksCommandHandler(IDrinksRepository drinksRepository, IEventBus eventBus)
        {
            _drinksRepository = drinksRepository;
            _eventBus = eventBus;
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
            var order = new Order();
            foreach (OrderDrinkDto orderDrinkDto in request.Orders)
            {
                order.OrderDrink(orderDrinkDto.Name, orderDrinkDto.Amount);
            }

            _eventBus.Publish(new OrderPlaced(order));

            return Task.FromResult(Unit.Value);
        }
    }
}