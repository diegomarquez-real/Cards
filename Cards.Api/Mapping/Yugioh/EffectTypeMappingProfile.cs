using AutoMapper;
using Cards.Api.Models.Yugioh;
using Cards.Api.Models.Yugioh.Create;
using Cards.Api.Models.Yugioh.Update;

namespace Cards.Api.Mapping.Yugioh
{
    public class EffectTypeMappingProfile : Profile
    {
        public EffectTypeMappingProfile()
        {
            CreateMap<Data.Models.Yugioh.EffectType, EffectTypeModel>();
            CreateMap<CreateEffectTypeModel, Data.Models.Yugioh.EffectType>();
            CreateMap<UpdateEffectTypeModel, Data.Models.Yugioh.EffectType>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}