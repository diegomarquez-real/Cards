using Flurl.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Api.Client.Clients.Yugioh
{
    public class CardClient : ClientBase, Abstractions.Clients.Yugioh.ICardClient
    {
        public CardClient(Abstractions.Settings.IApiClientSettings apiClientSettings,
            ILogger<CardClient> logger)
            : base(Enums.SchemaType.Yugioh, apiClientSettings, logger)
        {
        }

        public override string Name => "Cards";

        public Task<Guid> CreateCardAsync(Models.Yugioh.Create.CreateCardModel createCardModel)
        {
            return BuildUrlWithAuth()
                .PostJsonAsync(createCardModel)
                .ReceiveJson<Guid>();
        }
    }
}