using System;

namespace Bartender.Drinks.Application.Commands
{
    public class RenameDrink
    {
        public RenameDrink(Guid drinkId, string newName)
        {
            DrinkId = drinkId;
            NewName = newName;
        }

        public string NewName { get; }

        public Guid DrinkId { get; }
    }
}