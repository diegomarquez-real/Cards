namespace Cards.Api.Services.Yugioh.Abstractions
{
    public interface ICardService
    {
        Task<Models.Yugioh.CardModel> GetCardAsync(Guid cardId);
        Task<Models.Yugioh.CardModel> GetCardByNameAsync(string cardName);
        Task<List<Models.Yugioh.CardModel>> GetAllOrQueryAsync(
            Models.Yugioh.Query.CardQueryModel cardQueryModel,
            params Models.Yugioh.CardModel.Expansions[] expansions);
        Task<Guid> CreateCardAsync(Models.Yugioh.Create.CreateCardModel createCardModel);
        Task UpdateCardAsync(Data.Models.Yugioh.Card cardModel, Models.Yugioh.Update.UpdateCardModel updateCardModel);
        Task DeleteCardAsync(Guid cardId);
    }
}