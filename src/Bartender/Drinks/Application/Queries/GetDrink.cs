﻿using System;

namespace Bartender.Drinks.Application.Queries
{
    public class GetDrinkById
    {
        public GetDrinkById(Guid drinkId)
        {
            DrinkId = drinkId;
        }

        public Guid DrinkId { get; }
    }
}