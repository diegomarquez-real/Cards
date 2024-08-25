using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.WebScraper.Services
{
    public class YugiohService : Abstractions.IYugiohService
    {
        public YugiohService(IOptions<Options.AppSettingsOptions> options)
        {
            
        }
    }
}