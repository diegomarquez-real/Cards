using AutoMapper;

namespace Cards.Api.Services.Yugioh
{
    public class CardService : Abstractions.ICardService
    {
        private readonly IMapper _mapper;
        private readonly Data.Abstractions.Repositories.Yugioh.ICardRepository _cardRepository;

        public CardService(IMapper mapper,
            Data.Abstractions.Repositories.Yugioh.ICardRepository cardRepository)
        {
            _mapper = mapper;
            _cardRepository = cardRepository;
        }

        public async Task<Models.Yugioh.CardModel> GetCardAsync(Guid cardId)
        {
            var card = await _cardRepository.FindByIdAsync(cardId);

            return _mapper.Map<Models.Yugioh.CardModel>(card);
        }

        public async Task<Guid> CreateCardAsync(Models.Yugioh.Create.CreateCardModel createCardModel)
        {
            var card = _mapper.Map<Data.Models.Yugioh.Card>(createCardModel);
            var result = await _cardRepository.CreateAsync(card);

            return result.CardId;
        }

        public async Task UpdateCardAsync(Data.Models.Yugioh.Card cardModel, Models.Yugioh.Update.UpdateCardModel updateCardModel)
        {
            var card = _mapper.Map(updateCardModel, cardModel);

            await _cardRepository.UpdateAsync(card);
        }

        public async Task DeleteCardAsync(Guid cardId)
        {
            await _cardRepository.DeleteAsync(cardId);
        }
    }
}