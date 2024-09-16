using AutoMapper;

namespace Cards.Api.Services.Yugioh
{
    public class AttributeService : Abstractions.IAttributeService
    {
        private readonly IMapper _mapper;
        private readonly Data.Abstractions.Repositories.Yugioh.IAttributeRepository _attributeRepository;

        public AttributeService(IMapper mapper,
            Data.Abstractions.Repositories.Yugioh.IAttributeRepository attributeRepository)
        {
            _mapper = mapper;
            _attributeRepository = attributeRepository;
        }

        public async Task<Models.Yugioh.AttributeModel> GetAttributeAsync(Guid attributeId)
        {
            var attribute = await _attributeRepository.FindByIdAsync(attributeId);

            return _mapper.Map<Models.Yugioh.AttributeModel>(attribute);
        }

        public async Task<Guid> CreateAttributeAsync(Models.Yugioh.Create.CreateAttributeModel createAttributeModel)
        {
            var attribute = _mapper.Map<Data.Models.Yugioh.Attribute>(createAttributeModel);
            var result = await _attributeRepository.CreateAsync(attribute);

            return result.AttributeId;
        }

        public async Task UpdateAttributeAsync(Data.Models.Yugioh.Attribute attributeModel, Models.Yugioh.Update.UpdateAttributeModel updateAttributeModel)
        {
            var attribute = _mapper.Map(updateAttributeModel, attributeModel);

            await _attributeRepository.UpdateAsync(attribute);
        }

        public async Task DeleteAttributeAsync(Guid attributeId)
        {
            await _attributeRepository.DeleteAsync(attributeId);
        }
    }
}