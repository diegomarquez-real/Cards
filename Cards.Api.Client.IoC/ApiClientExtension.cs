using Flurl.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Api.Client.IoC
{
    public static class ApiClientExtension
    {
        public static void AddApiClient(this IServiceCollection services)
        {
            services.AddTransient<Abstractions.Dbo.IUserProfileClient, Dbo.UserProfileClient>();                     
            services.AddTransient<Abstractions.Yugioh.IAttributeClient, Yugioh.AttributeClient>();                     
            services.AddTransient<Abstractions.Yugioh.ISpeciesClient, Yugioh.SpeciesClient>();                     
            services.AddTransient<Abstractions.Yugioh.ICardClient, Yugioh.CardClient>();                     
            services.AddTransient<Abstractions.Yugioh.IPowerClient, Yugioh.PowerClient>();                     
            services.AddTransient<Abstractions.Yugioh.ISetClient, Yugioh.SetClient>();                     
            services.AddTransient<Abstractions.Yugioh.IEffectTypeClient, Yugioh.EffectTypeClient>();                     
            services.AddTransient<Abstractions.Yugioh.ICardEffectTypeAssociationClient, Yugioh.CardEffectTypeAssociationClient>();                     
            services.AddTransient<Abstractions.Yugioh.ICardSetAssociationClient, Yugioh.CardSetAssociationClient>();                     
            services.AddTransient<Abstractions.Yugioh.ICardSpeciesAssociationClient, Yugioh.CardSpeciesAssociationClient>();                     
        }
    }
}