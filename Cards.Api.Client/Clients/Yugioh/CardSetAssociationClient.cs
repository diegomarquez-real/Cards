using Flurl.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Api.Client.Clients.Yugioh
{
    public class CardSetAssociationClient : ClientBase, Abstractions.Clients.Yugioh.ICardSetAssociationClient
    {
        public CardSetAssociationClient(Abstractions.Settings.IApiClientSettings apiClientSettings,
            ILogger<CardSetAssociationClient> logger)
            : base(Enums.SchemaType.Yugioh, apiClientSettings, logger)
        {
        }

        public override string Name => "CardSetAssociations";

        public Task<Guid> CreateCardSetAssociationAsync(Models.Yugioh.Create.CreateCardSetAssociationModel createCardSetAssociationModel)
        {
            return BuildUrlWithAuth()
                .PostJsonAsync(createCardSetAssociationModel)
                .ReceiveJson<Guid>();
        }
    }
}