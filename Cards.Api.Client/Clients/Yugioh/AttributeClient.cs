using Flurl.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Api.Client.Yugioh
{
    public class AttributeClient : ClientBase, Abstractions.Yugioh.IAttributeClient
    {
        public AttributeClient(Abstractions.Settings.IApiClientSettings apiClientSettings,
            ILogger<AttributeClient> logger)
            : base(Enums.SchemaType.Yugioh, apiClientSettings, logger)
        {
        }

        public override string Name => "Attributes";

        public Task<Models.Yugioh.AttributeModel> GetAttributeByNameAsync(string attributeName)
        {
            return BuildUrlWithAuth()
                .AppendPathSegments("name", attributeName)
                .GetJsonAsync<Models.Yugioh.AttributeModel>();
        }

        public Task<Guid> CreateAttributeAsync(Models.Yugioh.Create.CreateAttributeModel createAttributeModel)
        {
            return BuildUrlWithAuth()
                .PostJsonAsync(createAttributeModel)
                .ReceiveJson<Guid>();
        } 
    }
}