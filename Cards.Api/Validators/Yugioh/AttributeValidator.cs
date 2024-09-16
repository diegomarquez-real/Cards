using FluentValidation;

namespace Cards.Api.Validators.Yugioh
{
    public class AttributeValidator : AbstractValidator<Data.Models.Yugioh.Attribute>
    {
        public AttributeValidator()
        {
            RuleFor(x => x.Name)
               .NotEmpty()
               .NotNull();
        }
    }
}