using System;

using Bartender.Api.Drinks.Domain;

namespace Bartender.Api.Drinks.Application.Queries
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