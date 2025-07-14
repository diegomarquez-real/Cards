using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace Cards.BlazorServer.Identity
{
    public class SessionService : Abstractions.ISessionService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly Abstractions.IUserClaimService _userClaimService;

        public SessionService(IHttpContextAccessor httpContextAccessor,
            Abstractions.IUserClaimService userClaimService)
        {
            _httpContextAccessor = httpContextAccessor;
            _userClaimService = userClaimService;
        }

        public async Task SignInAsync(Api.Models.Identity.AuthTokenModel authTokenModel)
        {
            var identity = CreateIdentity(authTokenModel.UserId, authTokenModel.Username, authTokenModel.Token);

            if(_httpContextAccessor.HttpContext == null)
                throw new ApplicationException("HttpContext is null.");

            await _httpContextAccessor.HttpContext.SignOutAsync();

            await _httpContextAccessor.HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity),
                new AuthenticationProperties() { IsPersistent = false });
        }

        public Task SignOutAsync()
        {
            if (_httpContextAccessor.HttpContext == null)
                throw new ApplicationException("HttpContext is null.");

           return _httpContextAccessor.HttpContext.SignOutAsync();
        }

        private ClaimsIdentity CreateIdentity(Guid userId, string username, string bearerToken)
        {
            var identity = new ClaimsIdentity(authenticationType: CookieAuthenticationDefaults.AuthenticationScheme,
                nameType: ClaimsIdentity.DefaultNameClaimType,
                roleType: ClaimsIdentity.DefaultRoleClaimType);

            var userDdClaim = _userClaimService.BuildUserProfileIdClaim(userId);
            identity.AddClaim(userDdClaim);

            var userNameClaim = _userClaimService.BuildUsernameClaim(username);
            identity.AddClaim(userNameClaim);

            var bearerTokenClaim = _userClaimService.BuildBearerTokenClaim(bearerToken);
            identity.AddClaim(bearerTokenClaim);

            return identity;
        }
    }
}