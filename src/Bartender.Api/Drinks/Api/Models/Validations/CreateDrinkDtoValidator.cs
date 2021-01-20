using FluentValidation;

namespace Bartender.Api.Drinks.Api.Models.Validations
{
    public class CreateDrinkDtoValidator : AbstractValidator<CreateDrinkDto>
    {
        public CreateDrinkDtoValidator()
        {
            RuleFor(x => x.Name).NotNull().MinimumLength(2);
        }
    }
}
