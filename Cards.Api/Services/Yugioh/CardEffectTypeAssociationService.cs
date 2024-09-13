using AutoMapper;

namespace Cards.Api.Services.Yugioh
{
    public class CardEffectTypeAssociationService : Abstractions.ICardEffectTypeAssociationService
    {
        private readonly IMapper _mapper;
        private readonly Data.Abstractions.Repositories.Yugioh.ICardEffectTypeAssociationRepository _cardEffectTypeAssociationRepository;

        public CardEffectTypeAssociationService(IMapper mapper,
            Data.Abstractions.Repositories.Yugioh.ICardEffectTypeAssociationRepository cardEffectTypeAssociationRepository)
        {
            _mapper = mapper;
            _cardEffectTypeAssociationRepository = cardEffectTypeAssociationRepository;
        }

        public async Task<Models.Yugioh.CardEffectTypeAssociationModel> GetCardEffectTypeAssociationAsync(Guid cardEffectTypeAssociationId)
        {
            var cardEffectTypeAssociation = await _cardEffectTypeAssociationRepository.FindByIdAsync(cardEffectTypeAssociationId);

            return _mapper.Map<Models.Yugioh.CardEffectTypeAssociationModel>(cardEffectTypeAssociation);
        }

        public async Task<Guid> CreateCardEffectTypeAssociationAsync(Models.Yugioh.Create.CreateCardEffectTypeAssociationModel createCardEffectTypeAssociationModel)
        {
            var cardEffectTypeAssociation = _mapper.Map<Data.Models.CardEffectTypeAssociation>(createCardEffectTypeAssociationModel);
            var result = await _cardEffectTypeAssociationRepository.CreateAsync(cardEffectTypeAssociation);

            return result.CardEffectTypeAssociationId;
        }

        public async Task DeleteCardEffectTypeAssociationAsync(Guid cardEffectTypeAssociationId)
        {
            await _cardEffectTypeAssociationRepository.DeleteAsync(cardEffectTypeAssociationId);
        }
    }
}