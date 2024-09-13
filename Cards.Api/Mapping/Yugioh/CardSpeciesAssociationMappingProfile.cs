using AutoMapper;

namespace Cards.Api.Mapping.Yugioh
{
    public class CardSpeciesAssociationMappingProfile : Profile 
    {
        public CardSpeciesAssociationMappingProfile()
        {
            CreateMap<Data.Models.Yugioh.CardSpeciesAssociation, Models.Yugioh.CardSpeciesAssociationModel>();
            CreateMap<Models.Yugioh.Create.CreateCardSpeciesAssociationModel, Data.Models.Yugioh.CardSpeciesAssociation>();
        }
    }
}