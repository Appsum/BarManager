using FluentValidation;

namespace Bartender.Api.Drinks.Api.Models.Validations
{
    public class RenameDrinkDtoValidator : AbstractValidator<RenameDrinkDto>
    {
        public RenameDrinkDtoValidator()
        {
            RuleFor(x => x.NewName).NotNull().MinimumLength(2);
        }
    }
}