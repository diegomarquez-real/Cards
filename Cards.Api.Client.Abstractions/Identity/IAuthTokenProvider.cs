using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Api.Client.Abstractions.Identity
{
    public interface IAuthTokenProvider
    {
        void OnTokenExpired();
        Models.Identity.AuthTokenModel? GetAuthToken();
        void AssignAuthToken(Models.Identity.AuthTokenModel authToken);
    }
}