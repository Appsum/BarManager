using System;
using System.Collections.Generic;

namespace Bartender.Api.Drinks.Application.Commands
{
    public class OrderDrinks
    {
        public ICollection<(string name, int amount)> Orders { get; }
        public OrderDrinks(ICollection<(string name, int amount)> orders)
        {
            Orders = orders;
        }
    }
}