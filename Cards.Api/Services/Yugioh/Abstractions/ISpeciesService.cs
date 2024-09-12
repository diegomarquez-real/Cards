namespace Cards.Api.Services.Yugioh.Abstractions
{
    public interface ISpeciesService
    {
        Task<Models.Yugioh.SpeciesModel> GetSpeciesAsync(Guid speciesId);
        Task<Guid> CreateSpeciesAsync(Models.Yugioh.Create.CreateSpeciesModel createSpeciesModel);
        Task UpdateSpeciesAsync(Models.Yugioh.SpeciesModel speciesModel, Models.Yugioh.Update.UpdateSpeciesModel updateSpeciesModel);
        Task DeleteSpeciesAsync(Guid speciesId);
    }
}