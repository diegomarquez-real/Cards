namespace Cards.Api.Services.Yugioh.Abstractions
{
    public interface IPowerService
    {
        Task<Models.Yugioh.PowerModel> GetPowerAsync(Guid powerId);
        Task<Guid> CreatePowerAsync(Models.Yugioh.Create.CreatePowerModel createPowerModel);
        Task UpdatePowerAsync(Data.Models.Yugioh.Power powerModel, Models.Yugioh.Update.UpdatePowerModel updatePowerModel);
        Task DeletePowerAsync(Guid powerId);
    }
}