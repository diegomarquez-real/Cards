using Flurl.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Api.Client.Yugioh
{
    public class PowerClient : ClientBase, Abstractions.Yugioh.IPowerClient
    {
        public PowerClient(Abstractions.Settings.IApiClientSettings apiClientSettings,
            ILogger<PowerClient> logger)
            : base(Enums.SchemaType.Yugioh, apiClientSettings, logger)
        {
        }

        public override string Name => "Powers";

        public Task<Guid> CreatePowerAsync(Models.Yugioh.Create.CreatePowerModel createPowerModel)
        {
            return BuildUrlWithAuth()
                .PostJsonAsync(createPowerModel)
                .ReceiveJson<Guid>();
        }
    }
}