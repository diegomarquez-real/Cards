namespace Cards.BlazorServer.Identity.Abstractions
{
    public interface IUserClaimService
    {
        System.Security.Claims.Claim BuildUserProfileIdClaim(Guid userId);
        Guid? GetCurrentUserProfileId();
        Guid GetCurrentUserProfileIdThrowIfMissing();
        System.Security.Claims.Claim BuildUsernameClaim(string username);
        string? GetCurrentUsername();
        string GetCurrentUsernameThrowIfMissing();
        System.Security.Claims.Claim BuildBearerTokenClaim(string sessionKey);
        string? GetCurrentBearerToken();
        string GetCurrentBearerTokenThrowIfMissing();
    }
}