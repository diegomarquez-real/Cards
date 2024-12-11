using Flurl.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Api.Client.Clients.Yugioh
{
    public class SetClient : ClientBase, Abstractions.Clients.Yugioh.ISetClient
    {
        public SetClient(Abstractions.Settings.IApiClientSettings apiClientSettings,
            ILogger<SetClient> logger)
            : base(Enums.SchemaType.Yugioh, apiClientSettings, logger)
        {
        }

        public override string Name => "Sets";

        public Task<Guid> CreateSetAsync(Models.Yugioh.Create.CreateSetModel createSetModel)
        {
            return BuildUrlWithAuth()
                .PostJsonAsync(createSetModel)
                .ReceiveJson<Guid>();
        }
    }
}