using System.Collections.Generic;

using Bartender.Api.Drinks.Application.Commands.Dtos;

namespace Bartender.Api.Drinks.Api.Models
{
    public class OrderDrinksDto
    {
        public IEnumerable<OrderDrinkDto> DrinksOrder { get; set; }
    }
}