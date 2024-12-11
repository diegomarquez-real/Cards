using AutoMapper;
using FluentValidation;

namespace Cards.Api.Services.Yugioh
{
    public class AttributeService : Abstractions.IAttributeService
    {
        private readonly IMapper _mapper;
        private readonly IValidator<Data.Models.Yugioh.Attribute> _validator;
        private readonly Data.Abstractions.Repositories.Yugioh.IAttributeRepository _attributeRepository;

        public AttributeService(IMapper mapper,
            IValidator<Data.Models.Yugioh.Attribute> validator,
            Data.Abstractions.Repositories.Yugioh.IAttributeRepository attributeRepository)
        {
            _mapper = mapper;
            _validator = validator;
            _attributeRepository = attributeRepository;
        }

        public async Task<Models.Yugioh.AttributeModel> GetAttributeAsync(Guid attributeId)
        {
            var attribute = await _attributeRepository.FindByIdAsync(attributeId);

            return _mapper.Map<Models.Yugioh.AttributeModel>(attribute);
        }

        public async Task<Models.Yugioh.AttributeModel> GetAttributeByNameAsync(string attributeName)
        {
            var attribute = await _attributeRepository.FindByNameAsync(attributeName);

            return _mapper.Map<Models.Yugioh.AttributeModel>(attribute);
        }

        public async Task<Guid> CreateAttributeAsync(Models.Yugioh.Create.CreateAttributeModel createAttributeModel)
        {
            var attribute = _mapper.Map<Data.Models.Yugioh.Attribute>(createAttributeModel);
            await _validator.ValidateAndThrowAsync(attribute);
            var result = await _attributeRepository.CreateAsync(attribute);

            return result.AttributeId;
        }

        public async Task UpdateAttributeAsync(Data.Models.Yugioh.Attribute attributeModel, Models.Yugioh.Update.UpdateAttributeModel updateAttributeModel)
        {
            var attribute = _mapper.Map(updateAttributeModel, attributeModel);
            await _validator.ValidateAndThrowAsync(attribute);
            await _attributeRepository.UpdateAsync(attribute);
        }

        public async Task DeleteAttributeAsync(Guid attributeId)
        {
            await _attributeRepository.DeleteAsync(attributeId);
        }
    }
}