using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Api.Models.Identity
{
    public class UserProfileLoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}