namespace Cards.Api.Services.Abstractions
{
    public interface IAttributeService
    {
        Task<Models.AttributeModel> GetAttributeAsync(Guid attributeId);
        Task<Guid> CreateAttributeAsync(Models.Create.CreateAttributeModel createAttributeModel);
        Task UpdateAttributeAsync(Models.AttributeModel attributeModel, Models.Update.UpdateAttributeModel updateAttributeModel);
        Task DeleteAttributeAsync(Guid attributeId);
    }
}