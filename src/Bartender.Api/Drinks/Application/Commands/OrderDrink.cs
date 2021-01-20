using System;

namespace Bartender.Api.Drinks.Application.Commands
{
    public class OrderDrink : ICommand
    {
        public OrderDrink(Guid drinkId, int amount)
        {
            DrinkId = drinkId;
        }

        public Guid DrinkId { get; }
        public int Amount { get; }
    }
}