using FluentValidation;

namespace Cards.Api.Validators.Yugioh
{
    public class SpeciesValidator : AbstractValidator<Data.Models.Yugioh.Species>
    {
        public SpeciesValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull();
        }
    }
}