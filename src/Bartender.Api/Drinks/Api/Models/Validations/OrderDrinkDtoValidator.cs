using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentValidation;

namespace Bartender.Api.Drinks.Api.Models.Validations
{
    public class OrderDrinkDtoValidator : AbstractValidator<OrderDrinkDto>
    {
        public OrderDrinkDtoValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEqual(Guid.Empty);
            RuleFor(x => x.Amount).GreaterThan(0).LessThanOrEqualTo(50);
        }
    }
}
