using AutoMapper;
using Cards.Api.Models.Yugioh;
using Cards.Api.Models.Yugioh.Create;
using Cards.Api.Models.Yugioh.Update;
using Cards.Data.Abstractions.Repositories.Yugioh;

namespace Cards.Api.Services.Yugioh
{
    public class EffectTypeService : Abstractions.IEffectTypeService
    {
        private readonly IMapper _mapper;
        private readonly IEffectTypeRepository _effectTypeRepository;

        public EffectTypeService(IMapper mapper,
             IEffectTypeRepository effectTypeRepository)
        {
            _mapper = mapper;
            _effectTypeRepository = effectTypeRepository;
        }

        public async Task<EffectTypeModel> GetEffectTypeAsync(Guid effectTypeId)
        {
            var effectType = await _effectTypeRepository.FindByIdAsync(effectTypeId);

            return _mapper.Map<EffectTypeModel>(effectType);
        }

        public async Task<Guid> CreateEffectTypeAsync(CreateEffectTypeModel createEffectTypeModel)
        {
            var effectType = _mapper.Map<Data.Models.EffectType>(createEffectTypeModel);
            var result = await _effectTypeRepository.CreateAsync(effectType);

            return result.EffectTypeId;
        }

        public async Task UpdateEffectTypeAsync(EffectTypeModel effectTypeModel, UpdateEffectTypeModel updateEffectTypeModel)
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