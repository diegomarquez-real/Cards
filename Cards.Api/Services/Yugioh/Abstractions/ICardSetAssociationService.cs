namespace Cards.Api.Services.Yugioh.Abstractions
{
    public interface ICardSetAssociationService
    {
        Task<Models.Yugioh.CardSetAssociationModel> GetCardSetAssociationAsync(Guid cardSetAssociationId);
        Task<Guid> CreateCardSetAssociationAsync(Models.Yugioh.Create.CreateCardSetAssociationModel createCardSetAssociationModel);
        Task DeleteCardSetAssociationAsync(Guid cardSetAssociationId);
    }
}