using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Data.Repositories.Dbo
{
    public class UserProfileRepository : GenericRepository<Models.Dbo.UserProfile, Guid>, Abstractions.Repositories.Dbo.IUserProfileRepository
    {
        public UserProfileRepository(Abstractions.IDataContext dataContext,
            Abstractions.IUserContext userContext)
            : base(dataContext, userContext)
        {
        }

        public async Task<Models.Dbo.UserProfile?> FindByUsernameAsync(string username)
        {
            try
            {
                var sql = @"SELECT u.*
                            FROM [dbo].[UserProfile] AS u
                            WHERE u.Username = @Username";

                return await base._dbConnection.QuerySingleOrDefaultAsync<Models.Dbo.UserProfile>(sql, new { Username = username });
            }
            catch (Exception) { throw; }
        }

        public async Task DeactivateUserProfileAsync(Guid userProfileId)
        {
            try
            {
                var parameters = new { UserProfileId = userProfileId, UpdatedOn = DateTimeOffset.UtcNow };
                string sql = @$"UPDATE [dbo].[UserProfile]
                                SET IsActive = 0, UpdatedOn = @UpdatedOn
                                WHERE UserProfileId = @UserProfileId";

                await base._dbConnection.ExecuteAsync(sql, parameters);
            }
            catch (Exception) { throw; }
        }
    }
}