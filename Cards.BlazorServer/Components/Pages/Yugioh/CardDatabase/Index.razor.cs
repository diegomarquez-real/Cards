using Cards.Api.Client.Abstractions.Yugioh;
using Cards.Api.Models.Yugioh;
using Cards.Api.Models.Yugioh.Query;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace Cards.BlazorServer.Components.Pages.Yugioh.CardDatabase
{
    public partial class Index
    {
        [Inject]
        ICardClient CardClient { get; set; }

        [SupplyParameterFromForm]
        public InputModel Input { get; set; } = new();

        private async Task SearchAsync()
        {
            List<CardModel> cards = await this.CardClient.GetAllOrQueryAsync(new CardQueryModel()
            {
                NameSearchText = Input.NameSearchText
            });
        }

        public class InputModel
        {
            public string NameSearchText { get; set; }
        }
    }
}