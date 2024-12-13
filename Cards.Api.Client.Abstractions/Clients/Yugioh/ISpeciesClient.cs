using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Api.Client.Abstractions.Yugioh
{
    public interface ISpeciesClient
    {
        Task<Models.Yugioh.SpeciesModel> GetSpeciesByNameAsync(string speciesName);
        Task<Guid> CreateSpeciesAsync(Models.Yugioh.Create.CreateSpeciesModel createSpeciesModel);
    }
}