using Cards.Data.Repositories.Yugioh;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Data.DependencyInjection
{
    public static class DataLayerExtension
    {
        public static void AddDataLayer(this IServiceCollection services, IConfiguration configuration)
        {
            // Configure settings for IOptions injection.
            services.Configure<Options.ConnectionStringOptions>(options =>
            {
                options.MssqlConnection = configuration.GetConnectionString("MssqlConnection");
            });

            services.AddScoped<Abstractions.IDataContext, DataContext>();

            // Using Scrutor to register all repositories.
            services.Scan(scan =>
                scan.FromAssemblyOf<CardRepository>() // The actual repository here doesn't matter, just using it to find which assembly our repositories are in.
                .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Repository")))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime());
        }
    }
}
