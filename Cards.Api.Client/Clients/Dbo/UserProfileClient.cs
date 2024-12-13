using Flurl.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Api.Client.Dbo
{
    public class UserProfileClient : ClientBase, Abstractions.Dbo.IUserProfileClient
    {
        public UserProfileClient(Abstractions.Settings.IApiClientSettings apiClientSettings,
            ILogger<UserProfileClient> logger)
            : base(Enums.SchemaType.Dbo, apiClientSettings, logger)
        {
        }

        public override string Name => "UserProfiles";

        public Task<Models.Identity.AuthTokenModel> AuthenticateAsync(Models.Identity.UserProfileLoginModel userProfileLoginModel)
        {
            return BuildUrlWithoutAuth()
                .AppendPathSegment("authenticate")
                .PostJsonAsync(userProfileLoginModel)
                .ReceiveJson<Models.Identity.AuthTokenModel>();

        }
    }
}