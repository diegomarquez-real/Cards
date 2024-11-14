using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.WebScraper.Services.Abstractions
{
    public interface IProgressService
    {
        void ProgressBar(int current, int total);
    }
}