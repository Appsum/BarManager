using System;

using Bartender.Drinks.Application.Commands.Dtos;

using FluentValidation;

namespace Bartender.Drinks.Api.Models.Validations
{
    public class OrderDrinkDtoValidator : AbstractValidator<OrderDrinkDto>
    {
        public OrderDrinkDtoValidator()
        {
            RuleFor(x => x.Id).NotEqual(Guid.Empty);
            RuleFor(x => x.Amount).GreaterThan(0).LessThanOrEqualTo(50);
        }
    }
}