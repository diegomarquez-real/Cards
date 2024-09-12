using AutoMapper;

namespace Cards.Api.Mapping.Yugioh
{
    public class PowerMappingProfile : Profile
    {
        public PowerMappingProfile()
        {
            CreateMap<Data.Models.Power, Models.Yugioh.PowerModel>();
            CreateMap<Models.Yugioh.Create.CreatePowerModel, Data.Models.Power>();
            CreateMap<Models.Yugioh.Update.UpdatePowerModel, Data.Models.Power>();
        }
    }
}