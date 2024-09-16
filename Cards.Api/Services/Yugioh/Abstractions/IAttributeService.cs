namespace Cards.Api.Services.Yugioh.Abstractions
{
    public interface IAttributeService
    {
        Task<Models.Yugioh.AttributeModel> GetAttributeAsync(Guid attributeId);
        Task<Guid> CreateAttributeAsync(Models.Yugioh.Create.CreateAttributeModel createAttributeModel);
        Task UpdateAttributeAsync(Data.Models.Yugioh.Attribute attributeModel, Models.Yugioh.Update.UpdateAttributeModel updateAttributeModel);
        Task DeleteAttributeAsync(Guid attributeId);
    }
}