using Cards.Api.Client.IoC;

namespace Cards.BlazorServer
{
    public static class ServicesExtension
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddApiClient();

            services.AddTransient<Api.Client.Abstractions.Settings.IApiClientSettings, Settings.ApiClientSettings>();

            services.AddScoped<Identity.Abstractions.IUserClaimService, Identity.UserClaimService>();
            services.AddScoped<Identity.Abstractions.IUserClaimService, Identity.UserClaimService>();
            services.AddScoped<Identity.Abstractions.ISessionService, Identity.SessionService>();
            services.AddScoped<Api.Client.Abstractions.Identity.IAuthTokenProvider, Identity.AuthTokenProvider>();
        }
    }
}