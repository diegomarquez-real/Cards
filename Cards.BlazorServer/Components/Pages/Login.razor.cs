using FluentValidation;
using Microsoft.AspNetCore.Components;

namespace Cards.BlazorServer.Components.Pages
{
    public partial class Login
    {
        [SupplyParameterFromForm]
        public InputModel Input { get; set; } = new();

        private async Task LoginAsync()
        {
            bool success = false;

            try
            {
                var authTokenModel = await UserProfileClient.AuthenticateAsync(new Api.Models.Identity.UserProfileLoginModel()
                {
                    Username = Input.Username,
                    Password = Input.Password
                });

                if (authTokenModel == null)
                    return;

                await SessionService.SignInAsync(authTokenModel);

                success = true;
            }
            catch (Exception ex)
            {
                //throw;
            }
            finally
            {
                if (success)
                {
                    NavigationManager.NavigateTo("/");
                }
            }
        }

        public class InputModel
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }

        public class InputValidator : AbstractValidator<InputModel>
        {
            public InputValidator()
            {
                RuleFor(x => x.Username)
                    .NotEmpty();

                RuleFor(x => x.Password)
                    .NotEmpty();
            }
        }
    }
}