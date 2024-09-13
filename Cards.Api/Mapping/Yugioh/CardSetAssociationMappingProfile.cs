using AutoMapper;

namespace Cards.Api.Mapping.Yugioh
{
    public class CardSetAssociationMappingProfile : Profile
    {
        public CardSetAssociationMappingProfile()
        {
            CreateMap<Data.Models.CardSetAssociation, Models.Yugioh.CardSetAssociationModel>();
            CreateMap<Models.Yugioh.Create.CreateCardSetAssociationModel, Data.Models.CardSetAssociation>();
        }
    }
}