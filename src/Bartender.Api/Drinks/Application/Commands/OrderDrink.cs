using System;

namespace BarManager.Api.Drinks.Application
{
    public class OrderDrink
    {
        public OrderDrink(Guid drinkId, int amount)
        {
            DrinkId = drinkId;
        }

        public Guid DrinkId { get; }
        public int Amount { get; }
    }
}