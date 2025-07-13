using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Api.Client.Abstractions.Yugioh
{
    public interface ICardClient
    {
        Task<Models.Yugioh.CardModel> GetCardByNameAsync(string cardName);
        Task<Guid> CreateCardAsync(Models.Yugioh.Create.CreateCardModel createCardModel);
        Task<List<Models.Yugioh.CardModel>> GetAllOrQueryAsync(Models.Yugioh.Query.CardQueryModel cardQueryModel);
    }
}