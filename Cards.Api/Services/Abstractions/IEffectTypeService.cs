namespace Cards.Api.Services.Abstractions
{
    public interface IEffectTypeService
    {
        Task<Models.EffectTypeModel> GetEffectTypeAsync(Guid effectTypeId);
        Task<Guid> CreateEffectTypeAsync(Models.Create.CreateEffectTypeModel createEffectTypeModel);
        Task UpdateEffectTypeAsync(Models.EffectTypeModel effectTypeModel, Models.Update.UpdateEffectTypeModel updateEffectTypeModel);
        Task DeleteEffectTypeAsync(Guid effectTypeId);
    }
}