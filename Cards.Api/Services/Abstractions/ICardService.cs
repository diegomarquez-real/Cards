namespace Cards.Api.Services.Abstractions
{
    public interface ICardService 
    {
        Task<Models.CardModel> GetCardAsync(Guid cardId);
        Task<Guid> CreateCardAsync(Models.Create.CreateCardModel createCardModel);
        Task UpdateCardAsync(Models.CardModel cardModel, Models.Update.UpdateCardModel updateCardModel);
        Task DeleteCardAsync(Guid cardId);
    }
}