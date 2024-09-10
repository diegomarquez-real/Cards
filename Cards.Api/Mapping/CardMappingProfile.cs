using AutoMapper;

namespace Cards.Api.Mapping
{
    public class CardMappingProfile : Profile
    {
        public CardMappingProfile()
        {
            CreateMap<Data.Models.Card, Models.CardModel>();
            CreateMap<Models.Create.CreateCardModel, Data.Models.Card>();
            CreateMap<Models.Update.UpdateCardModel, Data.Models.Card>();
        }
    }
}