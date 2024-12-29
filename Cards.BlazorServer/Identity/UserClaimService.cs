namespace Cards.BlazorServer.Identity
{
    public class UserClaimService : Abstractions.IUserClaimService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserClaimService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        #region UserId

        public System.Security.Claims.Claim BuildUserProfileIdClaim(Guid userId)
        {
            return new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Sid, userId.ToString());
        }

        public Guid? GetCurrentUserProfileId()
        {
            var claim = this.GetClaimByType(System.Security.Claims.ClaimTypes.Sid);

            return claim != null && Guid.TryParse(claim.Value, out Guid id) ? id : null;
        }

        public Guid GetCurrentUserProfileIdThrowIfMissing()
        {
            var userId = this.GetCurrentUserProfileId();

            return userId.HasValue ? userId.Value : throw new ApplicationException("User Claims failed to provide UserProfileId.");
        }

        #endregion

        #region Username

        public System.Security.Claims.Claim BuildUsernameClaim(string userName)
        {
            return new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Name, userName);
        }
        public string? GetCurrentUsername()
        {
            var claim = this.GetClaimByType(System.Security.Claims.ClaimTypes.Name);

            return claim?.Value;
        }

        public string GetCurrentUsernameThrowIfMissing()
        {
            var username = this.GetCurrentUsername();

            return !String.IsNullOrEmpty(username) ? username : throw new ApplicationException("User Claims failed to provide Username.");
        }

        #endregion

        #region BearerToken

        public System.Security.Claims.Claim BuildBearerTokenClaim(string bearerToken)
        {
            return new System.Security.Claims.Claim("BearerToken", bearerToken);
        }

        public string? GetCurrentBearerToken()
        {
            return this.GetClaimByType("BearerToken")?.Value;
        }

        public string GetCurrentBearerTokenThrowIfMissing()
        {
            var bearerToken = this.GetCurrentBearerToken();

            return !String.IsNullOrEmpty(bearerToken) ? bearerToken : throw new ApplicationException("User Claims failed to provide Bearer Token.");
        }

        #endregion

        private System.Security.Claims.Claim? GetClaimByType(string type)
        {
            if (_httpContextAccessor.HttpContext?.User.Identity == null)
                return null;

            var identity = (System.Security.Claims.ClaimsIdentity)_httpContextAccessor.HttpContext.User.Identity;

            return identity.FindFirst(x => x.Type == type);
        }  
    }
}