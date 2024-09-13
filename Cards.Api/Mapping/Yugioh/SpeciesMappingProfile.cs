using AutoMapper;

namespace Cards.Api.Mapping.Yugioh
{
    public class SpeciesMappingProfile : Profile
    {
        public SpeciesMappingProfile()
        {
            CreateMap<Data.Models.Yugioh.Species, Models.Yugioh.SpeciesModel>();
            CreateMap<Models.Yugioh.Create.CreateSpeciesModel, Data.Models.Yugioh.Species>();
            CreateMap<Models.Yugioh.Update.UpdateSpeciesModel, Data.Models.Yugioh.Species>();
        }
    }
}