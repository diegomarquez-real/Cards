using Flurl.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Api.Client.Yugioh
{
    public class SpeciesClient : ClientBase, Abstractions.Yugioh.ISpeciesClient
    {
        public SpeciesClient(Abstractions.Settings.IApiClientSettings apiClientSettings,
            ILogger<SpeciesClient> logger)
            : base(Enums.SchemaType.Yugioh, apiClientSettings, logger)
        {
        }

        public override string Name => "Species";

        public Task<Models.Yugioh.SpeciesModel> GetSpeciesByNameAsync(string speciesName)
        {
            return BuildUrlWithAuth()
                .AppendPathSegments("name", speciesName)
                .GetJsonAsync<Models.Yugioh.SpeciesModel>();
        }

        public Task<Guid> CreateSpeciesAsync(Models.Yugioh.Create.CreateSpeciesModel createSpeciesModel)
        {
            return BuildUrlWithAuth()
                .PostJsonAsync(createSpeciesModel)
                .ReceiveJson<Guid>();
        }
    }
}