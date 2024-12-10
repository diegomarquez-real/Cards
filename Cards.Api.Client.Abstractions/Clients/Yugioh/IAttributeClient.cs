﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Api.Client.Abstractions.Clients.Yugioh
{
    public interface IAttributeClient
    {
        Task<Guid> CreateAttributeAsync(Models.Yugioh.Create.CreateAttributeModel createAttributeModel);
    }
}