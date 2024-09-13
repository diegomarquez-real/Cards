using AutoMapper;
using Cards.Api.Models.Yugioh;
using Cards.Api.Models.Yugioh.Create;
using Cards.Api.Models.Yugioh.Update;
using Cards.Data.Abstractions.Repositories.Yugioh;

namespace Cards.Api.Services.Yugioh
{
    public class CardService : Abstractions.ICardService
    {
        private readonly IMapper _mapper;
        private readonly ICardRepository _cardRepository;

        public CardService(IMapper mapper,
            ICardRepository cardRepository)
        {
            _mapper = mapper;
            _cardRepository = cardRepository;
        }

        public async Task<CardModel> GetCardAsync(Guid cardId)
        {
            var card = await _cardRepository.FindByIdAsync(cardId);

            return _mapper.Map<CardModel>(card);
        }

        public async Task<Guid> CreateCardAsync(CreateCardModel createCardModel)
        {
            var card = _mapper.Map<Data.Models.Yugioh.Card>(createCardModel);
            var result = await _cardRepository.CreateAsync(card);

            return result.CardId;
        }

        public async Task UpdateCardAsync(CardModel cardModel, UpdateCardModel updateCardModel)
        {
            var card = _mapper.Map<Data.Models.Yugioh.Card>(updateCardModel);
            card.CardId = cardModel.CardId;

            await _cardRepository.UpdateAsync(card);
        }

        public async Task DeleteCardAsync(Guid cardId)
        {
            await _cardRepository.DeleteAsync(cardId);
        }
    }
}