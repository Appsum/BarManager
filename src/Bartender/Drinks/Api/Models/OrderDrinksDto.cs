using System.Collections.Generic;

using Bartender.Drinks.Application.Commands.Dtos;

namespace Bartender.Drinks.Api.Models
{
    public class OrderDrinksDto
    {
        public ICollection<OrderDrinkDto> DrinksOrder { get; set; }
    }
}