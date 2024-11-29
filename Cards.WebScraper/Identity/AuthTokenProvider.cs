using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.WebScraper.Identity
{
    public class AuthTokenProvider : Api.Client.Abstractions.Identity.IAuthTokenProvider
    {
        private static Api.Models.Identity.AuthTokenModel? _authToken;

        public AuthTokenProvider()
        {
        }

        public void AssignAuthToken(Api.Models.Identity.AuthTokenModel authToken)
        {
            _authToken = authToken;
        }

        public Api.Models.Identity.AuthTokenModel? GetAuthToken()
        {
            return _authToken;
        }

        public void OnTokenExpired()
        {
            // TODO: Implement token refresh.
            throw new NotImplementedException();
        }
    }
}