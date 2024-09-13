namespace Cards.Api.Services.Yugioh.Abstractions
{
    public interface ICardEffectTypeAssociationService
    {
        Task<Models.Yugioh.CardEffectTypeAssociationModel> GetCardEffectTypeAssociationAsync(Guid cardEffectTypeAssociationId);
        Task<Guid> CreateCardEffectTypeAssociationAsync(Models.Yugioh.Create.CreateCardEffectTypeAssociationModel createCardEffectTypeAssociationModel);
        Task DeleteCardEffectTypeAssociationAsync(Guid cardEffectTypeAssociationId);
    }
}