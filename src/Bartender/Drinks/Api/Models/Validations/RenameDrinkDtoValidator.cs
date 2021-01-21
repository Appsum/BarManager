using FluentValidation;

namespace Bartender.Drinks.Api.Models.Validations
{
    public class RenameDrinkDtoValidator : AbstractValidator<RenameDrinkDto>
    {
        public RenameDrinkDtoValidator()
        {
            RuleFor(x => x.NewName).NotNull().MinimumLength(2);
        }
    }
}