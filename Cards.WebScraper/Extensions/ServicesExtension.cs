using Cards.Api.Client.IoC;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.WebScraper
{
    public static class ServicesExtension
    {
        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<Services.Abstractions.IYugiohService, Services.YugiohService>();
            services.AddTransient<Services.Abstractions.IProgressService, Services.ProgressService>();
            services.AddApiClient(config =>
            {
                config.ApiBaseUrl = configuration.GetRequiredSection("AppSettings:apiBaseUrl").Value ?? String.Empty;
            });
        }
    }
}