﻿namespace Cards.Api.Services.Yugioh.Abstractions
{
    public interface ISetService
    {
        Task<Models.Yugioh.SetModel> GetSetAsync(Guid setId);
        Task<Guid> CreateSetAsync(Models.Yugioh.Create.CreateSetModel createSetModel);
        Task UpdateSetAsync(Data.Models.Yugioh.Set setModel, Models.Yugioh.Update.UpdateSetModel updateSetModel);
        Task DeleteSetAsync(Guid setId);
    }
}