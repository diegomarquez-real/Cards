﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Data.Abstractions
{ 
    public interface ICardRepository : IGenericRepository<Models.Card, Guid>
    {
    }
}