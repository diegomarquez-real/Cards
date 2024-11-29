using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Api.Client
{
    public abstract class ClientBase
    {
        private readonly Enums.SchemaType _schemaType;
        private readonly Abstractions.Settings.IApiClientSettings _apiClientSettings;
        private readonly Abstractions.Identity.IAuthTokenProvider _authTokenProvider;

        protected ClientBase(Enums.SchemaType schemaType, 
            Abstractions.Settings.IApiClientSettings apiClientSettings,
            ILogger logger)
        {
            _schemaType = schemaType;
            _apiClientSettings = apiClientSettings;
            _authTokenProvider = apiClientSettings.AuthTokenProvider;

            FlurlHttp.ConfigureClientForUrl(_apiClientSettings.ApiBaseUrl)
                .BeforeCall(x =>
                {
                })
                .AfterCall(x =>
                {
                })
                .WithHeaders(new
                {
                    Accept = "application/json"
                });
        }

        public abstract string Name { get; }

        protected IFlurlRequest BuildUrlWithAuth()
        {
            var authToken = _authTokenProvider.GetAuthToken();

            return _apiClientSettings.ApiBaseUrl
                .AppendPathSegment(this.AppendSchema(_schemaType))
                .AppendPathSegment(this.Name)
                .WithOAuthBearerToken(authToken?.Token ?? String.Empty);
        }

        protected Url BuildUrlWithoutAuth()
        {
            return _apiClientSettings.ApiBaseUrl
                .AppendPathSegment(this.AppendSchema(_schemaType))
                .AppendPathSegment(this.Name);
        }

        private string AppendSchema(Enums.SchemaType schemaType)
        {
            switch (schemaType)
            {
                case Enums.SchemaType.Dbo:
                    return String.Empty;
                case Enums.SchemaType.Yugioh:
                    return "Yugioh";
                default:
                    return String.Empty;
            }
        }
    }
}