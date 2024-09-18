using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Data.Abstractions.Repositories.Dbo
{
    public interface IUserProfileRepository : IGenericRepository<Models.Dbo.UserProfile, Guid>
    {
        Task DeactivateUserProfileAsync(Guid userProfileId);
    }
}