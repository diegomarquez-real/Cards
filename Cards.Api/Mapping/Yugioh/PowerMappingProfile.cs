using AutoMapper;

namespace Cards.Api.Mapping.Yugioh
{
    public class PowerMappingProfile : Profile
    {
        public PowerMappingProfile()
        {
            CreateMap<Data.Models.Yugioh.Power, Models.Yugioh.PowerModel>();
            CreateMap<Models.Yugioh.Create.CreatePowerModel, Data.Models.Yugioh.Power>();
            CreateMap<Models.Yugioh.Update.UpdatePowerModel, Data.Models.Yugioh.Power>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}