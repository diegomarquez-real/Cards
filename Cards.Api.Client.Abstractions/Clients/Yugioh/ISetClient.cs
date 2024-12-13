using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Api.Client.Abstractions.Yugioh
{
    public interface ISetClient
    {
        Task<Models.Yugioh.SetModel> GetSetByNameAsync(string setName);
        Task<Guid> CreateSetAsync(Models.Yugioh.Create.CreateSetModel createSetModel);
    }
}