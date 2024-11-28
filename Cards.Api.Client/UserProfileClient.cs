using Flurl.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Api.Client
{
    public class UserProfileClient : ClientBase, Abstractions.IUserProfileClient
    {
        public UserProfileClient(IOptions<Options.ApiClientSettings> options)
            : base(Enums.SchemaType.Dbo, options)
        { 
        }

        public override string Name => "UserProfiles";

        public Task<Models.Identity.AuthTokenModel> Authenticate(Models.Identity.UserProfileLoginModel userProfileLoginModel)
        {
            return base.BuildUrlWithAuth()
                .AppendPathSegment("authenticate")
                .PostJsonAsync(userProfileLoginModel)
                .ReceiveJson<Models.Identity.AuthTokenModel>();

        }
    }
}