namespace Cards.Api
{
    public static class OptionsExtension
    {
        public static void AddOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<Options.JWTSettingsOptions>(configuration.GetSection("JWTSettings"));
        }
    }
}