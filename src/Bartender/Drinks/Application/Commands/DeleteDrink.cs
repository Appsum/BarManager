using System;

namespace Bartender.Drinks.Application.Commands
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