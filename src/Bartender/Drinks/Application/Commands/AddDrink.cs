using System;

namespace Bartender.Drinks.Application.Commands
{
    public class AddDrink : ICommand
    {
        public AddDrink(Guid drinkId, string name)
        {
            DrinkId = drinkId;
            Name = name;
        }

        public Guid DrinkId { get; }
        public string Name { get; }
    }
}