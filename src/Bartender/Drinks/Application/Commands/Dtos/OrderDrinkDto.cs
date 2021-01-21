using System;

namespace Bartender.Api.Drinks.Application.Commands.Dtos
{
    public class OrderDrinkDto
    {
        public Guid Id { get; set; }
        public int Amount { get; set; }
    }
}