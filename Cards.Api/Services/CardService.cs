using AutoMapper;

namespace Cards.Api.Services
{
    public class CardService : Abstractions.ICardService
    {
        private readonly IMapper _mapper;
        private readonly Data.Abstractions.ICardRepository _cardRepository;

        public CardService(IMapper mapper,
            Data.Abstractions.ICardRepository cardRepository)
        {
            _mapper = mapper;
            _cardRepository = cardRepository;
        }

        public async Task<Models.CardModel> GetCardAsync(Guid cardId)
        {
            var card = await _cardRepository.FindByIdAsync(cardId);

            return _mapper.Map<Data.Models.Card, Models.CardModel>(card);
        }

        public async Task<Guid> CreateCardAsync(Models.Create.CreateCardModel createCardModel)
        {
            var card = _mapper.Map<Models.Create.CreateCardModel, Data.Models.Card>(createCardModel);
            var result = await _cardRepository.CreateAsync(card);

            return result.CardId;
        }

        public async Task UpdateCardAsync(Models.CardModel cardModel, Models.Update.UpdateCardModel updateCardModel)
        {
            var card = _mapper.Map<Models.Update.UpdateCardModel, Data.Models.Card>(updateCardModel);
            card.CardId = cardModel.CardId;

            await _cardRepository.UpdateAsync(card);
        }

        public async Task DeleteCardAsync(Guid cardId)
        {
            await _cardRepository.DeleteAsync(cardId);
        }
    }
}