namespace Cards.Api
{
    public static class ServicesExtension
    {
        public static void AddServices(this IServiceCollection services)
        {
            // Register Identity Services.
            services.AddTransient<Services.Identity.Abstractions.IUserAuthenticationService, Services.Identity.UserAuthenticationService>();
            services.AddScoped<Data.Abstractions.IUserContext, Services.Identity.UserContext>();
            services.AddScoped<Services.Identity.Abstractions.IUserClaimService, Services.Identity.UserClaimService>();

            // Register PasswordHasher for UserProfile.
            services.AddTransient<Microsoft.AspNetCore.Identity.IPasswordHasher<Data.Models.Dbo.UserProfile>, Microsoft.AspNetCore.Identity.PasswordHasher<Data.Models.Dbo.UserProfile>>();

            //Dbo
            services.AddScoped<Services.Dbo.Abstractions.IUserProfileService, Services.Dbo.UserProfileService>();

            // Yugioh
            services.AddScoped<Services.Yugioh.Abstractions.ICardService, Services.Yugioh.CardService>();
            services.AddScoped<Services.Yugioh.Abstractions.IAttributeService, Services.Yugioh.AttributeService>();
            services.AddScoped<Services.Yugioh.Abstractions.IEffectTypeService, Services.Yugioh.EffectTypeService>();
            services.AddScoped<Services.Yugioh.Abstractions.ISpeciesService, Services.Yugioh.SpeciesService> ();
            services.AddScoped<Services.Yugioh.Abstractions.ISetService, Services.Yugioh.SetService> ();
            services.AddScoped<Services.Yugioh.Abstractions.IPowerService, Services.Yugioh.PowerService> ();
            services.AddScoped<Services.Yugioh.Abstractions.ICardSpeciesAssociationService, Services.Yugioh.CardSpeciesAssociationService>();
            services.AddScoped<Services.Yugioh.Abstractions.ICardEffectTypeAssociationService, Services.Yugioh.CardEffectTypeAssociationService>();
            services.AddScoped<Services.Yugioh.Abstractions.ICardSetAssociationService, Services.Yugioh.CardSetAssociationService>();
        }
    }
}