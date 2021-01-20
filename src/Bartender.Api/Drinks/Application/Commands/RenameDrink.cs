﻿using System;

namespace Bartender.Api.Drinks.Application.Commands
{
    public class RenameDrink : ICommand
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