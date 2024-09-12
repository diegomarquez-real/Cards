namespace Cards.Api.Services.Yugioh.Abstractions
{
    public interface ICardService
    {
        Task<Models.Yugioh.CardModel> GetCardAsync(Guid cardId);
        Task<Guid> CreateCardAsync(Models.Yugioh.Create.CreateCardModel createCardModel);
        Task UpdateCardAsync(Models.Yugioh.CardModel cardModel, Models.Yugioh.Update.UpdateCardModel updateCardModel);
        Task DeleteCardAsync(Guid cardId);
    }
}