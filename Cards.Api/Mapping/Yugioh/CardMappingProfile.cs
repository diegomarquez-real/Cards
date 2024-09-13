﻿using AutoMapper;
using Cards.Api.Models.Yugioh;
using Cards.Api.Models.Yugioh.Create;
using Cards.Api.Models.Yugioh.Update;

namespace Cards.Api.Mapping.Yugioh
{
    public class CardMappingProfile : Profile
    {
        public CardMappingProfile()
        {
            CreateMap<Data.Models.Yugioh.Card, CardModel>();
            CreateMap<CreateCardModel, Data.Models.Yugioh.Card>();
            CreateMap<UpdateCardModel, Data.Models.Yugioh.Card>();
        }
    }
}