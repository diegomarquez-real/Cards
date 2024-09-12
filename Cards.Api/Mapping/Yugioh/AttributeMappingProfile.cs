using AutoMapper;
using Cards.Api.Models.Yugioh;
using Cards.Api.Models.Yugioh.Create;
using Cards.Api.Models.Yugioh.Update;

namespace Cards.Api.Mapping.Yugioh
{
    public class AttributeMappingProfile : Profile
    {
        public AttributeMappingProfile()
        {
            CreateMap<Data.Models.Attribute, AttributeModel>();
            CreateMap<CreateAttributeModel, Data.Models.Attribute>();
            CreateMap<UpdateAttributeModel, Data.Models.Attribute>();
        }
    }
}