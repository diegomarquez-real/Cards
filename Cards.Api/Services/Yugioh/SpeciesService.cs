using AutoMapper;

namespace Cards.Api.Services.Yugioh
{
    public class SpeciesService : Abstractions.ISpeciesService
    {
        private readonly IMapper _mapper;
        private readonly Data.Abstractions.Repositories.Yugioh.ISpeciesRepository _speciesRepository;

        public SpeciesService(IMapper mapper,
            Data.Abstractions.Repositories.Yugioh.ISpeciesRepository speciesRepository)
        {
            _mapper = mapper;
            _speciesRepository = speciesRepository;
        }

        public async Task<Models.Yugioh.SpeciesModel> GetSpeciesAsync(Guid speciesId)
        {
            var species = await _speciesRepository.FindByIdAsync(speciesId);

            return _mapper.Map<Models.Yugioh.SpeciesModel>(species);
        }

        public async Task<Guid> CreateSpeciesAsync(Models.Yugioh.Create.CreateSpeciesModel createSpeciesModel)
        {
            var species = _mapper.Map<Data.Models.Species>(createSpeciesModel);
            var result = await _speciesRepository.CreateAsync(species);

            return result.SpeciesId;
        }

        public Task UpdateSpeciesAsync(Models.Yugioh.SpeciesModel speciesModel, Models.Yugioh.Update.UpdateSpeciesModel updateSpeciesModel)
        {
            var species = _mapper.Map<Data.Models.Species>(updateSpeciesModel);
            species.SpeciesId = speciesModel.SpeciesId;

            return _speciesRepository.UpdateAsync(species);
        }

        public async Task DeleteSpeciesAsync(Guid speciesId)
        {
            await _speciesRepository.DeleteAsync(speciesId);
        }
    }
}