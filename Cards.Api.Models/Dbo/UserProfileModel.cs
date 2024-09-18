using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Api.Models.Dbo
{
    public class UserProfileModel
    {
        public Guid UserProfileId { get; set; }
        public string Username { get; set; }
        public string EmailAddress { get; set; }
        public string DisplayName { get; set; }
        public bool IsActive { get; set; }
    }
}