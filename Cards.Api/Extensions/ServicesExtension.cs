namespace Cards.Api
{
    public static class ServicesExtension
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<Services.Abstractions.ICardService, Services.CardService>();
            services.AddScoped<Services.Abstractions.IAttributeService, Services.AttributeService>();
        }
    }
}