using Cards.Api.Models.Yugioh;
using Microsoft.AspNetCore.Components;

namespace Cards.BlazorServer.Components.Pages.Yugioh
{
    public partial class DetailsCard
    {
        [Parameter]
        public CardModel Card { get; set; }

        protected override Task OnInitializedAsync()
        {
            return base.OnInitializedAsync();
        }
    }
}