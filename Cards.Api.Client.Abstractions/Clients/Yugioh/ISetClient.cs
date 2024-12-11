using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Api.Client.Abstractions.Clients.Yugioh
{
    public interface ISetClient
    {
        Task<Guid> CreateSetAsync(Models.Yugioh.Create.CreateSetModel createSetModel);
    }
}