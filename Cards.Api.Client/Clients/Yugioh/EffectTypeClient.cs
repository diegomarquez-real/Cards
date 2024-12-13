using Flurl.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Api.Client.Yugioh
{
    public class EffectTypeClient : ClientBase, Abstractions.Yugioh.IEffectTypeClient
    {
        public EffectTypeClient(Abstractions.Settings.IApiClientSettings apiClientSettings,
             ILogger<EffectTypeClient> logger)
            : base(Enums.SchemaType.Yugioh, apiClientSettings, logger)
        {
        }

        public override string Name => "EffectTypes";

        public Task<Models.Yugioh.EffectTypeModel> GetEffectTypeByName(string effectTypeName)
        {
            return BuildUrlWithAuth()
                .AppendPathSegments("name", effectTypeName)
                .GetJsonAsync<Models.Yugioh.EffectTypeModel>();
        }

        public Task<Guid> CreateEffectTypeAsync(Models.Yugioh.Create.CreateEffectTypeModel createEffectTypeModel)
        {
            return BuildUrlWithAuth()
                .PostJsonAsync(createEffectTypeModel)
                .ReceiveJson<Guid>();
        }
    }
}