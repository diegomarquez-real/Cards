namespace Cards.BlazorServer.Identity
{
    public class AuthTokenProvider : Api.Client.Abstractions.Identity.IAuthTokenProvider
    {
        private static Api.Models.Identity.AuthTokenModel? _authToken;
        private readonly Abstractions.IUserClaimService _userClaimService;
        private readonly Abstractions.ISessionService _sessionService;

        public AuthTokenProvider(Abstractions.IUserClaimService userClaimService,
            Abstractions.ISessionService sessionService)
        {
            _userClaimService = userClaimService;
            _sessionService = sessionService;
        }

        public void AssignAuthToken(Api.Models.Identity.AuthTokenModel authToken)
        {
            _authToken = authToken;
        }

        public Api.Models.Identity.AuthTokenModel? GetAuthToken()
        {
            if (_authToken == null)
            {
                return new Api.Models.Identity.AuthTokenModel()
                {
                    UserId = _userClaimService.GetCurrentUserProfileIdThrowIfMissing(),
                    Username = _userClaimService.GetCurrentUsernameThrowIfMissing(),
                    Token = _userClaimService.GetCurrentBearerTokenThrowIfMissing()
                };
            }
            else
            {
                return _authToken;
            }
        }

        public void OnTokenExpired()
        {
            _sessionService.SignOutAsync();
        }
    }
}