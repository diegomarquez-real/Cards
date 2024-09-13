using AutoMapper;

namespace Cards.Api.Mapping.Yugioh
{
    public class SetMappingProfile : Profile
    {
        public SetMappingProfile()
        {
            CreateMap<Data.Models.Yugioh.Set, Models.Yugioh.SetModel>();
            CreateMap<Models.Yugioh.Create.CreateSetModel, Data.Models.Yugioh.Set>();
            CreateMap<Models.Yugioh.Update.UpdateSetModel, Data.Models.Yugioh.Set>();
        }
    }
}