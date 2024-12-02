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
            services.AddTransient<Abstractions.Clients.Dbo.IUserProfileClient, Clients.Dbo.UserProfileClient>();                     
            services.AddTransient<Abstractions.Clients.Yugioh.IAttributeClient, Clients.Yugioh.AttributeClient>();                     
        }
    }
}