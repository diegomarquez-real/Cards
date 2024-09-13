using AutoMapper;

namespace Cards.Api.Services.Yugioh
{
    public class CardSetAssociationService : Abstractions.ICardSetAssociationService
    {
        private readonly IMapper _mapper;
        private readonly Data.Abstractions.Repositories.Yugioh.ICardSetAssociationRepository _cardSetAssociationRepository;

        public CardSetAssociationService(IMapper mapper,
            Data.Abstractions.Repositories.Yugioh.ICardSetAssociationRepository cardSetAssociationRepository)
        {
            _mapper = mapper;
            _cardSetAssociationRepository = cardSetAssociationRepository;
        }

        public async Task<Models.Yugioh.CardSetAssociationModel> GetCardSetAssociationAsync(Guid cardSetAssociationId)
        {
            var cardSetAssociation = await _cardSetAssociationRepository.FindByIdAsync(cardSetAssociationId);

            return _mapper.Map<Models.Yugioh.CardSetAssociationModel>(cardSetAssociation);
        }

        public async Task<Guid> CreateCardSetAssociationAsync(Models.Yugioh.Create.CreateCardSetAssociationModel createCardSetAssociationModel)
        {
            var cardSetAssociation = _mapper.Map<Data.Models.CardSetAssociation>(createCardSetAssociationModel);
            var result = await _cardSetAssociationRepository.CreateAsync(cardSetAssociation);

            return result.CardSetAssociationId;
        }

        public async Task DeleteCardSetAssociationAsync(Guid cardSetAssociationId)
        {
            await _cardSetAssociationRepository.DeleteAsync(cardSetAssociationId);
        }
    }
}