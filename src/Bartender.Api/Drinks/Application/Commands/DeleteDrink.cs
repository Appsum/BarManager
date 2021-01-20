using System;

namespace BarManager.Api.Drinks.Application
{
    public class DeleteDrink
    {
        public DeleteDrink(Guid drinkId)
        {
            DrinkId = drinkId;
        }

        public Guid DrinkId { get; }
    }
}