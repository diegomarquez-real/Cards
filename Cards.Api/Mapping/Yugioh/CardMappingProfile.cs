using AutoMapper;
using Cards.Api.Models.Yugioh;
using Cards.Api.Models.Yugioh.Create;
using Cards.Api.Models.Yugioh.Update;

namespace Cards.Api.Mapping.Yugioh
{
    public class CardMappingProfile : Profile
    {
        public CardMappingProfile()
        {
            CreateMap<Data.Models.Card, CardModel>();
            CreateMap<CreateCardModel, Data.Models.Card>();
            CreateMap<UpdateCardModel, Data.Models.Card>();
        }
    }
}