using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Api.Client.Abstractions.Dbo
{
    public interface IUserProfileClient
    {
        Task<Models.Identity.AuthTokenModel> AuthenticateAsync(Models.Identity.UserProfileLoginModel userProfileLoginModel);
    }
}