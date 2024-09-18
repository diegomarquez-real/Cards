namespace Cards.Api.Services.Identity.Abstractions
{
    public interface IUserAuthenticationService
    {
        Task<Models.Identity.AuthTokenModel?> AuthenticateAsync(Api.Models.Identity.UserProfileLoginModel userProfileLoginModel);
    }
}