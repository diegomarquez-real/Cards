using Microsoft.AspNetCore.Components;

namespace Cards.BlazorServer.Components.Pages
{
    public partial class LoginRedirect
    {
        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);

            NavigationManager.NavigateTo("/Login", false);
        }
    }
}