namespace Cards.BlazorServer.Identity.Abstractions
{
    public interface ISessionService
    {
        Task SignInAsync(Api.Models.Identity.AuthTokenModel authTokenModel);
        Task SignOutAsync();
    }
}