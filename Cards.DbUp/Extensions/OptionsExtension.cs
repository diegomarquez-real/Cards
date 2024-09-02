using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.DbUp
{
    public static class OptionsExtension
    {
        public static void AddOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<Options.ConnectionStringsOptions>(configuration.GetSection("ConnectionStrings"));
        }
    }
}
