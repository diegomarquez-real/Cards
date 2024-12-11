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
        public static IFlurlRequest ManageClient(this string clientUrl, ILogger logger)
        {
            return clientUrl.BeforeCall(x =>
            {
                logger.LogInformation($"Request: {x.HttpRequestMessage.RequestUri}");
            })
            .AfterCall(x =>
            {
                if (x.Succeeded)
                {
                    logger.LogInformation("Request succeeded.");
                }
                else
                {
                    logger.LogError("Request failed.");
                }
            })
            .WithHeaders(new
            {
                Accept = "application/json"
            });
        }
    }
}