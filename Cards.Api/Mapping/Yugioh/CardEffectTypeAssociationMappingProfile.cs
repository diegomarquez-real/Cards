using AutoMapper;

namespace Cards.Api.Mapping.Yugioh
{
    public class CardEffectTypeAssociationMappingProfile : Profile
    {
        public CardEffectTypeAssociationMappingProfile()
        {
            CreateMap<Data.Models.Yugioh.CardEffectTypeAssociation, Models.Yugioh.CardEffectTypeAssociationModel>();
            CreateMap<Models.Yugioh.Create.CreateCardEffectTypeAssociationModel, Data.Models.Yugioh.CardEffectTypeAssociation>();
        }
    }
}