using FluentValidation;

namespace Cards.Api.Validators.Dbo
{
    public class UserProfileItemValidator : AbstractValidator<Models.UserProfilePasswordModel>
    {
        public UserProfileItemValidator()
        {
            RuleFor(x => x.UserProfile)
               .SetValidator(new UserProfileValidator());

            RuleFor(x => x.Password)
               .SetValidator(new PasswordValidator());
        }
    }

    public class UserProfileValidator : AbstractValidator<Data.Models.Dbo.UserProfile>
    {
        public UserProfileValidator()
        {
            RuleFor(x => x.Username)
               .NotEmpty()
               .NotNull();

            RuleFor(x => x.DisplayName)
               .NotEmpty()
               .NotNull();

            RuleFor(x => x.EmailAddress)
               .NotEmpty()
               .EmailAddress();
        }
    }

    public class PasswordValidator : AbstractValidator<Models.PasswordModel>
    {
        public PasswordValidator()
        {
            RuleFor(x => x.Password)
               .NotEmpty().WithMessage("Password is required.")
               .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
               .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
               .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
               .Matches("[0-9]").WithMessage("Password must contain at least one number.")
               .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.")
               .Must(password => !password.Contains(" ")).WithMessage("Password must not contain spaces.");
        }
    }
}

namespace Cards.Api.Validators.Dbo.Models
{
    public class UserProfilePasswordModel
    {
        public Data.Models.Dbo.UserProfile UserProfile { get; set; }
        public PasswordModel Password { get; set; }
    }

    public class PasswordModel
    {
        public string Password { get; set; }
    }
}