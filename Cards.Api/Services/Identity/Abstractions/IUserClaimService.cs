namespace Cards.Api.Services.Identity.Abstractions
{
    public interface IUserClaimService
    {
        System.Security.Claims.Claim BuildUserProfileIdClaim(Guid userId);
        Guid? GetCurrentUserProfileId();
        Guid GetCurrentUserProfileIdThrowIfMissing();
        System.Security.Claims.Claim BuildUsernameClaim(string username);
        string? GetCurrentUsername();
        System.Security.Claims.Claim BuildSessionKeyClaim(string sessionKey);
        string? GetCurrentSessionKey();
    }
}