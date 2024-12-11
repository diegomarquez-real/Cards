using Cards.Api.Client.Extensions;
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
        private readonly ILogger _logger;
        private readonly Abstractions.Identity.IAuthTokenProvider _authTokenProvider;

        protected ClientBase(Enums.SchemaType schemaType, 
            Abstractions.Settings.IApiClientSettings apiClientSettings,
            ILogger logger)
        {
            _schemaType = schemaType;
            _apiClientSettings = apiClientSettings;
            _logger = logger;
            _authTokenProvider = apiClientSettings.AuthTokenProvider;
        }

        public abstract string Name { get; }

        protected IFlurlRequest BuildUrlWithAuth()
        {
            var authToken = _authTokenProvider.GetAuthToken();

            return _apiClientSettings.ApiBaseUrl
                .ManageClient(_logger)
                .AppendPathSegment(_schemaType.ToSchemaString())
                .AppendPathSegment(this.Name)
                .WithOAuthBearerToken(authToken?.Token ?? String.Empty);
        }

        protected IFlurlRequest BuildUrlWithoutAuth()
        {
            return _apiClientSettings.ApiBaseUrl
                .ManageClient(_logger)
                .AppendPathSegment(_schemaType.ToSchemaString())
                .AppendPathSegment(this.Name);
        }
    }
}