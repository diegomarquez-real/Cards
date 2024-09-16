using FluentValidation;

namespace Cards.Api.Validators.Yugioh
{
    public class SetValidator : AbstractValidator<Data.Models.Yugioh.Set>   
    {
        public SetValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull();
        }
    }
}