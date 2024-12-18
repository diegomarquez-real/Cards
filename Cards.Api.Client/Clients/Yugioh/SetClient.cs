using Flurl.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Api.Client.Yugioh
{
    public class SetClient : ClientBase, Abstractions.Yugioh.ISetClient
    {
        public SetClient(Abstractions.Settings.IApiClientSettings apiClientSettings,
            ILogger<SetClient> logger)
            : base(Enums.SchemaType.Yugioh, apiClientSettings, logger)
        {
        }

        public override string Name => "Sets";

        public Task<Models.Yugioh.SetModel> GetSetByNameAsync(string setName)
        {
            return BuildUrlWithAuth()
                .AppendPathSegments("name", Uri.EscapeDataString(setName))
                .GetJsonAsync<Models.Yugioh.SetModel>();
        }

        public Task<Guid> CreateSetAsync(Models.Yugioh.Create.CreateSetModel createSetModel)
        {
            return BuildUrlWithAuth()
                .PostJsonAsync(createSetModel)
                .ReceiveJson<Guid>();
        }
    }
}