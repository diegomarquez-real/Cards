using AutoMapper;
using Cards.Api.Models.Yugioh;
using Cards.Api.Models.Yugioh.Create;
using Cards.Api.Models.Yugioh.Update;
using Cards.Api.Services.Yugioh.Abstractions;
using Cards.Data.Abstractions.Repositories.Yugioh;

namespace Cards.Api.Services.Yugioh
{
    public class AttributeService : IAttributeService
    {
        private readonly IMapper _mapper;
        private readonly IAttributeRepository _attributeRepository;

        public AttributeService(IMapper mapper,
            IAttributeRepository attributeRepository)
        {
            _mapper = mapper;
            _attributeRepository = attributeRepository;
        }

        public async Task<AttributeModel> GetAttributeAsync(Guid attributeId)
        {
            var attribute = await _attributeRepository.FindByIdAsync(attributeId);

            return _mapper.Map<AttributeModel>(attribute);
        }

        public async Task<Guid> CreateAttributeAsync(CreateAttributeModel createAttributeModel)
        {
            var attribute = _mapper.Map<Data.Models.Attribute>(createAttributeModel);
            var result = await _attributeRepository.CreateAsync(attribute);

            return result.AttributeId;
        }

        public async Task UpdateAttributeAsync(AttributeModel attributeModel, UpdateAttributeModel updateAttributeModel)
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