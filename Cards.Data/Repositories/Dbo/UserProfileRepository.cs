using Cards.Data.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace Cards.Data.Repositories.Dbo
{
    public class UserProfileRepository : GenericRepository<Models.Dbo.UserProfile, Guid>, Abstractions.Repositories.Dbo.IUserProfileRepository
    {
        public UserProfileRepository(Abstractions.IDataContext dataContext)
            : base(dataContext)
        {
        }

        public async Task DeactivateUserProfileAsync(Guid userProfileId)
        {
            try
            {
                var parameters = new { UserProfileId = userProfileId, UpdatedOn = DateTimeOffset.UtcNow };
                string sql = @$"UPDATE [dbo].[UserProfile]
                                SET IsActive = 0, UpdatedOn = @UpdatedOn
                                WHERE UserProfileId = @UserProfileId";

                await _dbConnection.ExecuteAsync(sql, parameters);
            }
            catch (Exception) { throw; }
        }
    }
}