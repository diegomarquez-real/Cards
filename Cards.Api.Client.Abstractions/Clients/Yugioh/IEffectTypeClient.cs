using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Api.Client.Abstractions.Yugioh
{
    public interface IEffectTypeClient
    {
        Task<Models.Yugioh.EffectTypeModel> GetEffectTypeByName(string effectTypeName);
        Task<Guid> CreateEffectTypeAsync(Models.Yugioh.Create.CreateEffectTypeModel createEffectTypeModel);
    }
}