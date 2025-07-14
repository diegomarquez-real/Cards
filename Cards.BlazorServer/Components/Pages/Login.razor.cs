using Blazored.FluentValidation;
using FluentValidation;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace Cards.BlazorServer.Components.Pages
{
    public partial class Login
    {
        private EditContext _editContext;
        private ValidationMessageStore _validationMessageStore;

        [SupplyParameterFromForm]
        public InputModel Input { get; set; } = new();

        protected override void OnInitialized()
        {
            _editContext = new EditContext(Input);
            _validationMessageStore = new ValidationMessageStore(_editContext);
        }

        private async Task LoginAsync()
        {
            bool success = false;
            try
            {
                var authTokenModel = await UserProfileClient.AuthenticateAsync(new Api.Models.Identity.UserProfileLoginModel()
                {
                    Username = this.Input.Username,
                    Password = this.Input.Password
                });
                if (authTokenModel == null)
                {
                    return;
                }
                await SessionService.SignInAsync(authTokenModel);
                success = true;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Failed to login.");
                _validationMessageStore.Add(_editContext.Field(""), "Username or Password is incorrect.");
                _editContext.NotifyValidationStateChanged();
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