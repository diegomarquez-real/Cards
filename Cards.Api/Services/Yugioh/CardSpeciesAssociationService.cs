using AutoMapper;

namespace Cards.Api.Services.Yugioh
{
    public class CardSpeciesAssociationService : Abstractions.ICardSpeciesAssociationService
    {
        private readonly IMapper _mapper;
        private readonly Data.Abstractions.Repositories.Yugioh.ICardSpeciesAssociationRepository _cardSpeciesAssociationRepository;

        public CardSpeciesAssociationService(IMapper mapper,
           Data.Abstractions.Repositories.Yugioh.ICardSpeciesAssociationRepository cardSpeciesAssociationRepository)
        {
            _mapper = mapper;
            _cardSpeciesAssociationRepository = cardSpeciesAssociationRepository;
        }

        public async Task<Models.Yugioh.CardSpeciesAssociationModel> GetCardSpeciesAssociationAsync(Guid cardSpeciesAssociationId)
        {
            var cardSpeciesAssociation = await _cardSpeciesAssociationRepository.FindByIdAsync(cardSpeciesAssociationId);

            return _mapper.Map<Models.Yugioh.CardSpeciesAssociationModel>(cardSpeciesAssociation);
        }

        public async Task<Guid> CreateCardSpeciesAssociationAsync(Models.Yugioh.Create.CreateCardSpeciesAssociationModel createCardSpeciesAssociationModel)
        {
            var cardSpeciesAssociation = _mapper.Map<Data.Models.Yugioh.CardSpeciesAssociation>(createCardSpeciesAssociationModel);
            var result = await _cardSpeciesAssociationRepository.CreateAsync(cardSpeciesAssociation);

            return result.CardSpeciesAssociationId;
        }

        public async Task DeleteCardSpeciesAssociationAsync(Guid cardSpeciesAssociationId)
        {
            await _cardSpeciesAssociationRepository.DeleteAsync(cardSpeciesAssociationId);
        }     
    }
}