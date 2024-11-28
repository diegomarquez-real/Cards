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
        public static void AddApiClient(this IServiceCollection services, Action<Configuration> config)
        {
            var opts = new Configuration();
            config(opts);

            services.Configure<Options.ApiClientSettings>(x =>
            {
                x.ApiBaseUrl = opts.ApiBaseUrl;
            });

            services.AddTransient<Abstractions.IUserProfileClient, UserProfileClient>();

            FlurlHttp.ConfigureClientForUrl(opts.ApiBaseUrl)
                .BeforeCall(x =>
                {

                })
                .AfterCall(x =>
                {

                })
                .WithHeaders(new 
                {
                    Accept = "application/json"
                });
                               
        }
    }
}