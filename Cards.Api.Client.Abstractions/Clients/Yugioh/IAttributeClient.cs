using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Api.Client.Abstractions.Yugioh
{
    public interface IAttributeClient
    {
        Task<Models.Yugioh.AttributeModel> GetAttributeByNameAsync(string attributeName);
        Task<Guid> CreateAttributeAsync(Models.Yugioh.Create.CreateAttributeModel createAttributeModel);
    }
}