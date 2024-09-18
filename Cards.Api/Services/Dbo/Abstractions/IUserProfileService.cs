namespace Cards.Api.Services.Dbo.Abstractions
{
    public interface IUserProfileService
    {
        Task<Models.Dbo.UserProfileModel> GetUserProfileAsync(Guid userProfileId);
        Task<Guid> CreateUserProfileAsync(Models.Dbo.Create.CreateUserProfileModel createUserProfileModel);
        Task UpdateUserProfileAsync(Data.Models.Dbo.UserProfile userProfileModel, Models.Dbo.Update.UpdateUserProfileModel updateUserProfileModel);
        Task DeleteUserProfileAsync(Guid userProfileId);
    }
}