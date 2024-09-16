using AutoMapper;

namespace Cards.Api.Services.Yugioh
{
    public class SetService : Abstractions.ISetService
    {
        private readonly IMapper _mapper;
        private readonly Data.Abstractions.Repositories.Yugioh.ISetRepository _setRepository;

        public SetService(IMapper mapper,
            Data.Abstractions.Repositories.Yugioh.ISetRepository setRepository)
        {
            _mapper = mapper;
            _setRepository = setRepository;
        }

        public async Task<Models.Yugioh.SetModel> GetSetAsync(Guid setId)
        {
            var set = await _setRepository.FindByIdAsync(setId);

            return _mapper.Map<Models.Yugioh.SetModel>(set);
        }

        public async Task<Guid> CreateSetAsync(Models.Yugioh.Create.CreateSetModel createSetModel)
        {
            var set = _mapper.Map<Data.Models.Yugioh.Set>(createSetModel);
            var result = await _setRepository.CreateAsync(set);

            return result.SetId;
        }

        public async Task UpdateSetAsync(Data.Models.Yugioh.Set setModel, Models.Yugioh.Update.UpdateSetModel updateSetModel)
        {
            var set = _mapper.Map(updateSetModel, setModel);

            await _setRepository.UpdateAsync(set);
        }

        public async Task DeleteSetAsync(Guid setId)
        {
            await _setRepository.DeleteAsync(setId);
        }
    }
}