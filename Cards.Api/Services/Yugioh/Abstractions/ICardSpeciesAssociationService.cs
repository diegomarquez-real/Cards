namespace Cards.Api.Services.Yugioh.Abstractions
{
    public interface ICardSpeciesAssociationService
    {
        Task<Models.Yugioh.CardSpeciesAssociationModel> GetCardSpeciesAssociationAsync(Guid cardSpeciesAssociationId);
        Task<Guid> CreateCardSpeciesAssociationAsync(Models.Yugioh.Create.CreateCardSpeciesAssociationModel createCardSpeciesAssociationModel);
        Task DeleteCardSpeciesAssociationAsync(Guid cardSpeciesAssociationId);
    }
}