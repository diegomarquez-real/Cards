﻿namespace Cards.Api.Services.Yugioh.Abstractions
{
    public interface IEffectTypeService
    {
        Task<Models.Yugioh.EffectTypeModel> GetEffectTypeAsync(Guid effectTypeId);
        Task<Guid> CreateEffectTypeAsync(Models.Yugioh.Create.CreateEffectTypeModel createEffectTypeModel);
        Task UpdateEffectTypeAsync(Models.Yugioh.EffectTypeModel effectTypeModel, Models.Yugioh.Update.UpdateEffectTypeModel updateEffectTypeModel);
        Task DeleteEffectTypeAsync(Guid effectTypeId);
    }
}