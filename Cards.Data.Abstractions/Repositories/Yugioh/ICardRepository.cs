﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Data.Abstractions.Repositories.Yugioh
{
    public interface ICardRepository : IGenericRepository<Models.Yugioh.Card, Guid>
    {
        Task<Models.Yugioh.Card?> FindByNameAsync(string cardName);
        Task<IEnumerable<Models.Yugioh.Card>> FindAllOrQueryAsync(Models.Yugioh.CardQuery cardQuery);
    }
}