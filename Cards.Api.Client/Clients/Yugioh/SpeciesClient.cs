using Flurl.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Api.Client.Clients.Yugioh
{
    public class SpeciesClient : ClientBase, Abstractions.Clients.Yugioh.ISpeciesClient
    {
        public SpeciesClient(Abstractions.Settings.IApiClientSettings apiClientSettings,
            ILogger<SpeciesClient> logger)
            : base(Enums.SchemaType.Yugioh, apiClientSettings, logger)
        {
        }

        public override string Name => "Species";

        public Task<Guid> CreateSpeciesAsync(Models.Yugioh.Create.CreateSpeciesModel createSpeciesModel)
        {
            return BuildUrlWithAuth()
                .PostJsonAsync(createSpeciesModel)
                .ReceiveJson<Guid>();
        }
    }
}