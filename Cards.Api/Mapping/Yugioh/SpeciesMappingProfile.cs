using AutoMapper;

namespace Cards.Api.Mapping.Yugioh
{
    public class SpeciesMappingProfile : Profile
    {
        public SpeciesMappingProfile()
        {
            CreateMap<Data.Models.Species, Models.Yugioh.SpeciesModel>();
            CreateMap<Models.Yugioh.Create.CreateSpeciesModel, Data.Models.Species>();
            CreateMap<Models.Yugioh.Update.UpdateSpeciesModel, Data.Models.Species>();
        }
    }
}