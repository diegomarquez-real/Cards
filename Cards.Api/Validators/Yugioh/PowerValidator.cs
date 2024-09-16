using FluentValidation;

namespace Cards.Api.Validators.Yugioh
{
    public class PowerValidator : AbstractValidator<Data.Models.Yugioh.Power>
    {
        public PowerValidator()
        {
            RuleFor(x => x.Attack)
                .NotEmpty()
                .NotNull();
            RuleFor(x => x.Defense)
                .NotEmpty()
                .NotNull();
        }
    }
}