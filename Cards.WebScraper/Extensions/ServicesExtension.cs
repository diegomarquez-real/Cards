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
        public static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<Services.Abstractions.IYugiohService, Services.YugiohService>();
            services.AddTransient<Services.Abstractions.IProgressService, Services.ProgressService>();
        }
    }
}