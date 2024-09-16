using FluentValidation;

namespace Cards.Api.Validators.Yugioh
{
    public class EffectTypeValidator : AbstractValidator<Data.Models.Yugioh.EffectType>
    {
        public EffectTypeValidator()
        {
            RuleFor(x => x.Name)
               .NotEmpty()
               .NotNull();
        }
    }
}