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
            CreateMap<Data.Models.EffectType, EffectTypeModel>();
            CreateMap<CreateEffectTypeModel, Data.Models.EffectType>();
            CreateMap<UpdateEffectTypeModel, Data.Models.EffectType>();
        }
    }
}