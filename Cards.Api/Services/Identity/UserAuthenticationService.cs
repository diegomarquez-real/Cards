using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Cards.Api.Services.Identity
{
    public class UserAuthenticationService : Abstractions.IUserAuthenticationService
    {
        private readonly IOptions<Options.JWTSettingsOptions> _jwtSettingsOptions;
        private readonly Dbo.Abstractions.IUserProfileService _userService;
        private readonly Abstractions.IUserClaimService _userClaimService;

        public UserAuthenticationService(IOptions<Options.JWTSettingsOptions> jwtSettingsOptions,
            Dbo.Abstractions.IUserProfileService userService,
            Abstractions.IUserClaimService userClaimService)
        {
            _jwtSettingsOptions = jwtSettingsOptions;
            _userService = userService;
            _userClaimService = userClaimService;
        }

        public async Task<Models.Identity.AuthTokenModel?> AuthenticateAsync(Models.Identity.UserProfileLoginModel userProfileLoginModel)
        {
            var loginResult = await _userService.LoginAsync(userProfileLoginModel);

            if (loginResult.LoginResultCode != LoginResultCode.Success)
                return null;

            var userProfile = loginResult.UserProfile;
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettingsOptions.Value.Token));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    _userClaimService.BuildUserProfileIdClaim(userProfile.UserProfileId),
                    _userClaimService.BuildUsernameClaim(userProfile.Username),
                }),
                Expires = DateTime.UtcNow.AddMinutes(_jwtSettingsOptions.Value.ExpirationInMinutes),
                SigningCredentials = credentials,
                Issuer = _jwtSettingsOptions.Value.Issuer,
                Audience = _jwtSettingsOptions.Value.Audience
            };

            var handler = new JwtSecurityTokenHandler();
            var token = handler.CreateToken(tokenDescriptor);

            return new Models.Identity.AuthTokenModel()
            {
                UserId = userProfile.UserProfileId,
                Username = userProfile.Username,
                Token = handler.WriteToken(token)
            };
        }
    }
}