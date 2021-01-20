using System.Collections.Generic;

namespace Bartender.Api.Drinks.Api.Models
{
    public class OrderDrinksDto
    {
        public IEnumerable<OrderDrinkDto> DrinksOrder { get; set; }
    }
}