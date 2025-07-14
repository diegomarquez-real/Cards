using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Api.Client.Extensions
{
    public static class FlurlExtensions
    {
        public static IFlurlRequest ManageClient(this string clientUrl, ILogger logger, Abstractions.Identity.IAuthTokenProvider authTokenProvider)
        {
            return clientUrl.BeforeCall(x =>
            {
                logger.LogDebug($"Begin Call To {x.Request.Url}.");
            })
            .AfterCall(x =>
            {
                logger.LogDebug($"End Call To {x.Request.Url} Which Took {x.Duration?.TotalSeconds} Seconds.");
            })
            .OnError(x =>
            {
                if (x.HttpResponseMessage?.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    authTokenProvider.OnTokenExpired();
                }
            })
            .WithHeaders(new
            {
                Accept = "application/json"
            })
            .WithSettings(x =>
            {
                x.JsonSerializer = new Flurl.Http.Configuration.DefaultJsonSerializer(new System.Text.Json.JsonSerializerOptions()
                {
                    DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
                    PropertyNameCaseInsensitive = true,
                    IgnoreReadOnlyProperties = true
                });
            });
        }
    }
}