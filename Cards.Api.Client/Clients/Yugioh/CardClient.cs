using Flurl.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Api.Client.Yugioh
{
    public class CardClient : ClientBase, Abstractions.Yugioh.ICardClient
    {
        public CardClient(Abstractions.Settings.IApiClientSettings apiClientSettings,
            ILogger<CardClient> logger)
            : base(Enums.SchemaType.Yugioh, apiClientSettings, logger)
        {
        }

        public override string Name => "Cards";

        public Task<Models.Yugioh.CardModel> GetCardByNameAsync(string cardName)
        {
            return BuildUrlWithAuth()
                .AppendPathSegments("name", Uri.EscapeDataString(cardName))
                .GetJsonAsync<Models.Yugioh.CardModel>();
        }

        public Task<Guid> CreateCardAsync(Models.Yugioh.Create.CreateCardModel createCardModel)
        {
            return BuildUrlWithAuth()
                .PostJsonAsync(createCardModel)
                .ReceiveJson<Guid>();
        }

        public Task<List<Models.Yugioh.CardModel>> GetAllOrQueryAsync(Models.Yugioh.Query.CardQueryModel cardQueryModel)
        {
            return BuildUrlWithAuth()
                .SetQueryParams(cardQueryModel)
                .GetJsonAsync<List<Models.Yugioh.CardModel>>();
        }
    }
}