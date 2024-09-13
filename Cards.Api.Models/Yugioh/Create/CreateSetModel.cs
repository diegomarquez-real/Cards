using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Api.Models.Yugioh.Create
{
    public class CreateSetModel
    {
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}