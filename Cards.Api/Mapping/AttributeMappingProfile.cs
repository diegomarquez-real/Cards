using AutoMapper;

namespace Cards.Api.Mapping
{
    public class AttributeMappingProfile : Profile
    {
        public AttributeMappingProfile()
        {
            CreateMap<Data.Models.Attribute, Models.AttributeModel>();
            CreateMap<Models.Create.CreateAttributeModel, Data.Models.Attribute>();
            CreateMap<Models.Update.UpdateAttributeModel, Data.Models.Attribute>();
        }
    }
}