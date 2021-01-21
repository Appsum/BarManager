using System;

using Bartender.Drinks.Domain;

namespace Bartender.Drinks.Application.Queries
{
    public class GetDrinkById : IQuery<Drink>
    {
        public GetDrinkById(Guid drinkId)
        {
            DrinkId = drinkId;
        }

        public Guid DrinkId { get; }
    }
}