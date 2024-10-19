namespace Cards.Api.Services.Identity
{
    public class UserContext : Data.Abstractions.IUserContext
    {
        private readonly Abstractions.IUserClaimService _userClaimService;

        public UserContext(Abstractions.IUserClaimService userClaimService)
        {
            _userClaimService = userClaimService;
        }

        private Guid? _currentUserIdentifier;
        public Guid CurrentUserIdentifier
        {
            get
            {
                if (!_currentUserIdentifier.HasValue)
                {
                    _currentUserIdentifier = _userClaimService.GetCurrentUserProfileIdThrowIfMissing();
                }

                return _currentUserIdentifier.Value;
            }
        }
    }
}
