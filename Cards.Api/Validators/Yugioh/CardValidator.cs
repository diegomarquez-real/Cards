using FluentValidation;

namespace Cards.Api.Validators.Yugioh
{
    public class CardValidator : AbstractValidator<Data.Models.Yugioh.Card>
    {
        public CardValidator()
        {
            RuleFor(x => x.Name)
               .NotEmpty()
               .NotNull();

            RuleFor(x => x.Description)
               .NotEmpty()
               .NotNull();
        }
    }
}