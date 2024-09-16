using AutoMapper;
using FluentValidation;

namespace Cards.Api.Services.Yugioh
{
    public class EffectTypeService : Abstractions.IEffectTypeService
    {
        private readonly IMapper _mapper;
        private readonly IValidator<Data.Models.Yugioh.EffectType> _validator;
        private readonly Data.Abstractions.Repositories.Yugioh.IEffectTypeRepository _effectTypeRepository;

        public EffectTypeService(IMapper mapper,
             IValidator<Data.Models.Yugioh.EffectType> validator,
             Data.Abstractions.Repositories.Yugioh.IEffectTypeRepository effectTypeRepository)
        {
            _mapper = mapper;
            _validator = validator;
            _effectTypeRepository = effectTypeRepository;
        }

        public async Task<Models.Yugioh.EffectTypeModel> GetEffectTypeAsync(Guid effectTypeId)
        {
            var effectType = await _effectTypeRepository.FindByIdAsync(effectTypeId);

            return _mapper.Map<Models.Yugioh.EffectTypeModel>(effectType);
        }

        public async Task<Guid> CreateEffectTypeAsync(Models.Yugioh.Create.CreateEffectTypeModel createEffectTypeModel)
        {
            var effectType = _mapper.Map<Data.Models.Yugioh.EffectType>(createEffectTypeModel);
            await _validator.ValidateAndThrowAsync(effectType);
            var result = await _effectTypeRepository.CreateAsync(effectType);

            return result.EffectTypeId;
        }

        public async Task UpdateEffectTypeAsync(Data.Models.Yugioh.EffectType effectTypeModel, Models.Yugioh.Update.UpdateEffectTypeModel updateEffectTypeModel)
        {
            var effectType = _mapper.Map(updateEffectTypeModel, effectTypeModel);
            await _validator.ValidateAndThrowAsync(effectType);
            await _effectTypeRepository.UpdateAsync(effectType);
        }

        public async Task DeleteEffectTypeAsync(Guid effectTypeId)
        {
            await _effectTypeRepository.DeleteAsync(effectTypeId);
        }
    }
}