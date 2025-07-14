using Microsoft.Extensions.Options;

namespace Cards.BlazorServer.Settings
{
    public class ApiClientSettings : Api.Client.Abstractions.Settings.IApiClientSettings
    {
        public ApiClientSettings(IOptions<Options.AppSettingsOptions> appSettingsOptions,
            Api.Client.Abstractions.Identity.IAuthTokenProvider authTokenProvider)
        {
            this.ApiBaseUrl = appSettingsOptions.Value.ApiBaseUrl;
            this.AuthTokenProvider = authTokenProvider;
        }

        public string ApiBaseUrl { get; }
        public Api.Client.Abstractions.Identity.IAuthTokenProvider AuthTokenProvider { get; }
    }
}