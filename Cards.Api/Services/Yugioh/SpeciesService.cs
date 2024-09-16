using AutoMapper;
using FluentValidation;

namespace Cards.Api.Services.Yugioh
{
    public class SpeciesService : Abstractions.ISpeciesService
    {
        private readonly IMapper _mapper;
        private readonly IValidator<Data.Models.Yugioh.Species> _validator;
        private readonly Data.Abstractions.Repositories.Yugioh.ISpeciesRepository _speciesRepository;

        public SpeciesService(IMapper mapper,
            IValidator<Data.Models.Yugioh.Species> validator,
            Data.Abstractions.Repositories.Yugioh.ISpeciesRepository speciesRepository)
        {
            _mapper = mapper;
            _validator = validator;
            _speciesRepository = speciesRepository;
        }

        public async Task<Models.Yugioh.SpeciesModel> GetSpeciesAsync(Guid speciesId)
        {
            var species = await _speciesRepository.FindByIdAsync(speciesId);

            return _mapper.Map<Models.Yugioh.SpeciesModel>(species);
        }

        public async Task<Guid> CreateSpeciesAsync(Models.Yugioh.Create.CreateSpeciesModel createSpeciesModel)
        {
            var species = _mapper.Map<Data.Models.Yugioh.Species>(createSpeciesModel);
            await _validator.ValidateAndThrowAsync(species);
            var result = await _speciesRepository.CreateAsync(species);

            return result.SpeciesId;
        }

        public async Task UpdateSpeciesAsync(Data.Models.Yugioh.Species speciesModel, Models.Yugioh.Update.UpdateSpeciesModel updateSpeciesModel)
        {
            var species = _mapper.Map(updateSpeciesModel, speciesModel);
            await _validator.ValidateAndThrowAsync(species);
            await _speciesRepository.UpdateAsync(species);
        }

        public async Task DeleteSpeciesAsync(Guid speciesId)
        {
            await _speciesRepository.DeleteAsync(speciesId);
        }
    }
}