using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Api.Models.Identity
{
    public class AuthTokenModel
    {
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
    }
}