namespace Cards.BlazorServer.Extensions
{
    public static class OptionsExtension
    {
        public static void RegisterOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<Options.AppSettingsOptions>(configuration.GetSection("AppSettings"));
        }
    }
}