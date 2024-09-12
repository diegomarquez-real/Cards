using AutoMapper;

namespace Cards.Api.Services
{
    public class EffectTypeService : Abstractions.IEffectTypeService
    {
        private readonly IMapper _mapper;
        private readonly Data.Abstractions.IEffectTypeRepository _effectTypeRepository;

        public EffectTypeService(IMapper mapper,
             Data.Abstractions.IEffectTypeRepository effectTypeRepository)
        {
            _mapper = mapper;
            _effectTypeRepository = effectTypeRepository;
        }

        public async Task<Models.EffectTypeModel> GetEffectTypeAsync(Guid effectTypeId)
        {
            var effectType = await _effectTypeRepository.FindByIdAsync(effectTypeId);

            return _mapper.Map<Models.EffectTypeModel>(effectType);
        }

        public async Task<Guid> CreateEffectTypeAsync(Models.Create.CreateEffectTypeModel createEffectTypeModel)
        {
            var effectType = _mapper.Map<Data.Models.EffectType>(createEffectTypeModel);
            var result = await _effectTypeRepository.CreateAsync(effectType);

            return result.EffectTypeId;
        }

        public async Task UpdateEffectTypeAsync(Models.EffectTypeModel effectTypeModel, Models.Update.UpdateEffectTypeModel updateEffectTypeModel)
        {
            var effectType = _mapper.Map<Data.Models.EffectType>(updateEffectTypeModel);
            effectType.EffectTypeId = effectTypeModel.EffectTypeId;

            await _effectTypeRepository.UpdateAsync(effectType);
        }

        public async Task DeleteEffectTypeAsync(Guid effectTypeId)
        {
            await _effectTypeRepository.DeleteAsync(effectTypeId);
        }
    }
}