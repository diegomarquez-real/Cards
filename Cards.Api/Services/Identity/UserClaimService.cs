namespace Cards.Api.Services.Identity
{
    public class UserClaimService : Abstractions.IUserClaimService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserClaimService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        #region UserProfileId

        public System.Security.Claims.Claim BuildUserProfileIdClaim(Guid userId)
        {
            return new System.Security.Claims.Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub, userId.ToString());
        }

        public Guid GetCurrentUserProfileIdThrowIfMissing()
        {
            var userId = this.GetCurrentUserProfileId();

            return userId.HasValue ? userId.Value : throw new ApplicationException("User Claims failed to provide UserProfileId.");
        }

        public Guid? GetCurrentUserProfileId()
        {
            var idClaim = this.GetClaimByType(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub);

            if (idClaim == null)
                return null;

            return Guid.TryParse(idClaim.Value, out Guid id) ? id : null;
        }

        #endregion

        #region Username

        public System.Security.Claims.Claim BuildUsernameClaim(string userName)
        {
            return new System.Security.Claims.Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Name, userName);
        }

        public string? GetCurrentUsername()
        {
            var claim = this.GetClaimByType(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Name);

            return claim?.Value;
        }

        #endregion

        #region SessionKey

        public System.Security.Claims.Claim BuildSessionKeyClaim(string sessionKey)
        {
            return new System.Security.Claims.Claim("session_key", sessionKey);
        }

        public string? GetCurrentSessionKey()
        {
            var claim = this.GetClaimByType("session_key");

            return claim?.Value;
        }

        #endregion

        private System.Security.Claims.Claim? GetClaimByType(string type)
        {
            // This is where we actually read the claims from the Identity User.
            var identity = (System.Security.Claims.ClaimsIdentity?)_httpContextAccessor?.HttpContext?.User.Identity;

            return identity?.FindFirst(x => x.Type == type);
        }
    }
}