using AutoMapper;

namespace Cards.Api.Mapping
{
    public class EffectTypeMappingProfile : Profile
    {
        public EffectTypeMappingProfile()
        {
            CreateMap<Data.Models.EffectType, Models.EffectTypeModel>();
            CreateMap<Models.Create.CreateEffectTypeModel, Data.Models.EffectType>();
            CreateMap<Models.Update.UpdateEffectTypeModel, Data.Models.EffectType>();
        }
    }
}