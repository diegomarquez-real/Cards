using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Api.Client.Abstractions.Settings
{
    public interface IApiClientSettings
    {
        string ApiBaseUrl { get; }
        Identity.IAuthTokenProvider AuthTokenProvider { get; }
    }
}