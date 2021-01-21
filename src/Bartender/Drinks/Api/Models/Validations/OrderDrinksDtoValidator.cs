using FluentValidation;

namespace Bartender.Drinks.Api.Models.Validations
{
    public class OrderDrinksDtoValidator : AbstractValidator<OrderDrinksDto>
    {
        public OrderDrinksDtoValidator()
        {
            RuleFor(x => x.DrinksOrder).NotNull();
        }
    }
}