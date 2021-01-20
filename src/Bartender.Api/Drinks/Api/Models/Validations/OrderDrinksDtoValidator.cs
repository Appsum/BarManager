using FluentValidation;

namespace Bartender.Api.Drinks.Api.Models.Validations
{
    public class OrderDrinksDtoValidator : AbstractValidator<OrderDrinksDto>
    {
        public OrderDrinksDtoValidator()
        {
            RuleFor(x => x.DrinksOrder).NotNull();
        }
    }
}