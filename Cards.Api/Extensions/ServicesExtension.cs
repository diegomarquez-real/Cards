namespace Cards.Api
{
    public static class ServicesExtension
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<Services.Yugioh.Abstractions.ICardService, Services.Yugioh.CardService>();
            services.AddScoped<Services.Yugioh.Abstractions.IAttributeService, Services.Yugioh.AttributeService>();
            services.AddScoped<Services.Yugioh.Abstractions.IEffectTypeService, Services.Yugioh.EffectTypeService>();
            services.AddScoped<Services.Yugioh.Abstractions.ISpeciesService, Services.Yugioh.SpeciesService> ();
        }
    }
}