namespace Cards.Api.Services.Yugioh.Abstractions
{
    public interface ISpeciesService
    {
        Task<Models.Yugioh.SpeciesModel> GetSpeciesAsync(Guid speciesId);
        Task<Models.Yugioh.SpeciesModel> GetSpeciesByNameAsync(string speciesName);
        Task<Guid> CreateSpeciesAsync(Models.Yugioh.Create.CreateSpeciesModel createSpeciesModel);
        Task UpdateSpeciesAsync(Data.Models.Yugioh.Species speciesModel, Models.Yugioh.Update.UpdateSpeciesModel updateSpeciesModel);
        Task DeleteSpeciesAsync(Guid speciesId);
    }
}