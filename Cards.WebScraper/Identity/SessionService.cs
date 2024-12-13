using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.WebScraper.Identity
{
    public class SessionService : Abstractions.ISessionService
    {
        private readonly IOptions<Options.DefaultCredentialsOptions> _options;
        private readonly Api.Client.Abstractions.Dbo.IUserProfileClient _userProfileClient;
        private readonly Api.Client.Abstractions.Identity.IAuthTokenProvider _authTokenProvider;

        public SessionService(IOptions<Options.DefaultCredentialsOptions> options,
            Api.Client.Abstractions.Dbo.IUserProfileClient userProfileClient,
            Api.Client.Abstractions.Identity.IAuthTokenProvider authTokenProvider)
        {
            _options = options;
            _userProfileClient = userProfileClient;
            _authTokenProvider = authTokenProvider;
        }

        public async Task LoginAsync(string username, string password)
        {
            // If the username and password are the default credentials, return the default token
            // TODO: Implement a custom login credentials.
            var authTokenModel = await _userProfileClient.AuthenticateAsync(new Api.Models.Identity.UserProfileLoginModel()
            {
                Username = _options.Value.Username,
                Password = _options.Value.Password
            });

            if(authTokenModel == null)
            {
                // TODO: Handle invalid login credentials.
                return;
            }

            _authTokenProvider.AssignAuthToken(authTokenModel);
        }
    }
}