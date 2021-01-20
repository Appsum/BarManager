using System;

using FluentValidation;

namespace Bartender.Api.Drinks.Api.Models.Validations
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