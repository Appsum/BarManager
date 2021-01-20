using System;

namespace BarManager.Api.Drinks.Application
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