using System.Collections.Generic;

using Bartender.Drinks.Application.Commands.Dtos;

namespace Bartender.Drinks.Application.Commands
{
    public class OrderDrinks : ICommand
    {
        public ICollection<OrderDrinkDto> Orders { get; }
        public OrderDrinks(ICollection<OrderDrinkDto> orders)
        {
            Orders = orders;
        }
    }
}