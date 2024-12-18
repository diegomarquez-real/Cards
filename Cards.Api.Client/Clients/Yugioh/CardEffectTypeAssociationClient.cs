using Flurl.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Api.Client.Yugioh
{
    public class CardEffectTypeAssociationClient : ClientBase, Abstractions.Yugioh.ICardEffectTypeAssociationClient
    {
        public CardEffectTypeAssociationClient(Abstractions.Settings.IApiClientSettings apiClientSettings,
            ILogger<CardEffectTypeAssociationClient> logger)
            : base(Enums.SchemaType.Yugioh, apiClientSettings, logger)
        {
        }

        public override string Name => "CardEffectTypeAssociations";

        public Task<Guid> CreateCardEffectTypeAssociationAsync(Models.Yugioh.Create.CreateCardEffectTypeAssociationModel createCardEffectTypeAssociationModel)
        {
            return BuildUrlWithAuth()
                .PostJsonAsync(createCardEffectTypeAssociationModel)
                .ReceiveJson<Guid>();
        }
    }
}