﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Data.Abstractions.Repositories.Yugioh
{
    public interface IAttributeRepository : IGenericRepository<Models.Yugioh.Attribute, Guid>
    {
        Task<Models.Yugioh.Attribute?> FindByNameAsync(string attributeName);
    }
}