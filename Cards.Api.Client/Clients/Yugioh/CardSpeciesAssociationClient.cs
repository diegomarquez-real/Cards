using Flurl.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Api.Client.Yugioh
{
    public class CardSpeciesAssociationClient : ClientBase, Abstractions.Yugioh.ICardSpeciesAssociationClient
    {
        public CardSpeciesAssociationClient(Abstractions.Settings.IApiClientSettings apiClientSettings,
            ILogger<CardSpeciesAssociationClient> logger)
            : base(Enums.SchemaType.Yugioh, apiClientSettings, logger)
        {
        }

        public override string Name => "CardSpeciesAssociations";

        public Task<Guid> CreateCardSpeciesAssociationAsync(Models.Yugioh.Create.CreateCardSpeciesAssociationModel createCardSpeciesAssociationModel)
        {
            return BuildUrlWithAuth()
                .PostJsonAsync(createCardSpeciesAssociationModel)
                .ReceiveJson<Guid>();
        }
    }
}