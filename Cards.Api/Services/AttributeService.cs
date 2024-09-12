using AutoMapper;

namespace Cards.Api.Services
{
    public class AttributeService : Abstractions.IAttributeService
    {
        private readonly IMapper _mapper;
        private readonly Data.Abstractions.IAttributeRepository _attributeRepository;

        public AttributeService(IMapper mapper, 
            Data.Abstractions.IAttributeRepository attributeRepository)
        {
            _mapper = mapper;
            _attributeRepository = attributeRepository;
        }

        public async Task<Models.AttributeModel> GetAttributeAsync(Guid attributeId)
        {
            var attribute = await _attributeRepository.FindByIdAsync(attributeId);

            return _mapper.Map<Models.AttributeModel>(attribute);
        }

        public async Task<Guid> CreateAttributeAsync(Models.Create.CreateAttributeModel createAttributeModel)
        {
            var attribute = _mapper.Map<Data.Models.Attribute>(createAttributeModel);
            var result = await _attributeRepository.CreateAsync(attribute);

            return result.AttributeId;
        }

        public async Task UpdateAttributeAsync(Models.AttributeModel attributeModel, Models.Update.UpdateAttributeModel updateAttributeModel)
        {
            var attribute = _mapper.Map<Data.Models.Attribute>(updateAttributeModel);
            attribute.AttributeId = attributeModel.AttributeId;

            await _attributeRepository.UpdateAsync(attribute);
        }

        public async Task DeleteAttributeAsync(Guid attributeId)
        {
            await _attributeRepository.DeleteAsync(attributeId);
        }
    }
}