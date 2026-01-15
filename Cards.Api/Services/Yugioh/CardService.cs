using AutoMapper;
using Cards.Api.Options;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace Cards.Api.Services.Yugioh
{
    public class CardService : Abstractions.ICardService
    {
        private readonly IMapper _mapper;
        private readonly IValidator<Data.Models.Yugioh.Card> _validator;
        private readonly Data.Abstractions.Repositories.Yugioh.ICardRepository _cardRepository;
        private readonly IOptions<AppSettingsOptions> _appSettingsOptions;

        public CardService(IMapper mapper,
            IValidator<Data.Models.Yugioh.Card> validator,
            Data.Abstractions.Repositories.Yugioh.ICardRepository cardRepository,
            IOptions<AppSettingsOptions> appSettingsOptions)
        {
            _mapper = mapper;
            _validator = validator;
            _cardRepository = cardRepository;
            _appSettingsOptions = appSettingsOptions;
        }

        public async Task<Models.Yugioh.CardModel> GetCardAsync(Guid cardId)
        {
            var card = await _cardRepository.FindByIdAsync(cardId);

            return _mapper.Map<Models.Yugioh.CardModel>(card);
        }

        public async Task<Models.Yugioh.CardModel> GetCardByNameAsync(string cardName)
        {
            var card = await _cardRepository.FindByNameAsync(cardName);

            return _mapper.Map<Models.Yugioh.CardModel>(card);
        }

        public async Task<List<Models.Yugioh.CardModel>> GetAllOrQueryAsync(
            Models.Yugioh.Query.CardQueryModel cardQueryModel,
            params Models.Yugioh.CardModel.Expansions[] expansions)
        {
            var cardQuery = _mapper.Map<Data.Models.Yugioh.CardQuery>(cardQueryModel);
            var cards = await _cardRepository.FindAllOrQueryAsync(cardQuery);
            List<Models.Yugioh.CardModel> cardModels = _mapper.Map<List<Models.Yugioh.CardModel>>(cards);
            this.ExpandCardModels(cardModels, expansions);

            return cardModels;
        }

        public async Task<Guid> CreateCardAsync(Models.Yugioh.Create.CreateCardModel createCardModel)
        {
            var card = _mapper.Map<Data.Models.Yugioh.Card>(createCardModel);
            await _validator.ValidateAndThrowAsync(card);
            var result = await _cardRepository.CreateAsync(card);

            return result.CardId;
        }

        public async Task UpdateCardAsync(Data.Models.Yugioh.Card cardModel, Models.Yugioh.Update.UpdateCardModel updateCardModel)
        {
            var card = _mapper.Map(updateCardModel, cardModel);
            await _validator.ValidateAndThrowAsync(card);
            await _cardRepository.UpdateAsync(card);
        }

        public async Task DeleteCardAsync(Guid cardId)
        {
            await _cardRepository.DeleteAsync(cardId);
        }

        private void ExpandCardModels(List<Models.Yugioh.CardModel> cards, 
            Models.Yugioh.CardModel.Expansions[] expansions)
        {
            if(expansions?.Any() != true || expansions?.Any() != true)
            {
                return;
            }

            foreach(Models.Yugioh.CardModel.Expansions expansion in expansions)
            {
                switch (expansion)
                {
                    case Models.Yugioh.CardModel.Expansions.Images:
                        string cardImgsDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), _appSettingsOptions.Value.YugiohImgRelativeDir);
                        if (!Directory.Exists(cardImgsDir))
                        {
                            break;
                        }
                        string imgDir = String.Empty;
                        cards.ForEach(card =>
                        {
                            imgDir = Path.Combine(cardImgsDir, card.CardId.ToString());
                            if (!Directory.Exists(imgDir))
                            {
                                return; 
                            }
                            IEnumerable<string> imgPaths = Directory.EnumerateFiles(imgDir);
                            if(imgPaths?.Any() != true)
                            {
                                return;
                            }
                            card.ImageBytes = imgPaths.Select(x => File.ReadAllBytes(x));
                        });
                        break;
                }
            }
        }
    }
}