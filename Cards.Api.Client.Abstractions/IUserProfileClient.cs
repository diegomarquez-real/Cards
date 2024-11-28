using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Api.Client.Abstractions
{
    public interface IUserProfileClient
    {
        Task<Models.Identity.AuthTokenModel> Authenticate(Models.Identity.UserProfileLoginModel userProfileLoginModel);
    }
}