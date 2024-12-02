using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.WebScraper.Identity.Abstractions
{
    public interface ISessionService
    {
        Task LoginAsync(string username, string password);
    }
}