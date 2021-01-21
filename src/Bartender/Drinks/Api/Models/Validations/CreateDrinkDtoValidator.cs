using FluentValidation;

namespace Bartender.Drinks.Api.Models.Validations
{
    public class CreateDrinkDtoValidator : AbstractValidator<CreateDrinkDto>
    {
        public CreateDrinkDtoValidator()
        {
            RuleFor(x => x.Name).NotNull().MinimumLength(2);
        }
    }
}