using System;

namespace Bartender.Api.Drinks.Api.Models
{
    public class OrderDrinkDto
    {
        public Guid Id { get; set; }
        public int Amount { get; set; }
    }
}
