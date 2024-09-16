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
            CreateMap<Data.Models.Yugioh.Attribute, AttributeModel>();
            CreateMap<CreateAttributeModel, Data.Models.Yugioh.Attribute>();
            CreateMap<UpdateAttributeModel, Data.Models.Yugioh.Attribute>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}