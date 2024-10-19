using Dapper;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Data.Repositories
{
    public class GenericRepository<TEntity, TPrimaryKeyType> : Abstractions.Repositories.IGenericRepository<TEntity, TPrimaryKeyType> where TEntity : class
    {
        protected readonly System.Data.IDbConnection _dbConnection;
        private readonly Abstractions.IUserContext _userContext;

        public GenericRepository(Abstractions.IDataContext dataContext,
            Abstractions.IUserContext userContext)
        {
            _dbConnection = dataContext.CreateConnection();
            _userContext = userContext;
        }

        public async Task<TEntity> FindByIdAsync(TPrimaryKeyType entityId)
        {
            try
            {
                string sql = @$"SELECT * 
                                FROM {GenericExtensions.GetTableName<TEntity>()} 
                                WHERE {GenericExtensions.GetKeyColumnName<TEntity>()} = @EntityId";

                return await _dbConnection.QuerySingleAsync<TEntity>(sql, new { EntityId = entityId });
            }
            catch (Exception) { throw; }
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            try
            {
                string sql = @$"SELECT * 
                                FROM {GenericExtensions.GetTableName<TEntity>()}";

                return await _dbConnection.QueryAsync<TEntity>(sql);
            }
            catch (Exception) { throw; }
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            try
            {
                this.TrySetCreatedBy(entity);
                this.TrySetCreatedOnDate(entity);

                string sql = @$"INSERT INTO {GenericExtensions.GetTableName<TEntity>()} ({GenericExtensions.GetColumns<TEntity>(excludeKey: true)})
                                OUTPUT INSERTED.*
                                VALUES ({GenericExtensions.GetPropertyNames<TEntity>(excludeKey: true)})";

                return await _dbConnection.QuerySingleAsync<TEntity>(sql, entity);
            }
            catch (Exception) { throw; }
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            try
            {
                this.TrySetUpdatedBy(entity);
                this.TrySetUpdatedOnDate(entity);

                string sql = @$"UPDATE {GenericExtensions.GetTableName<TEntity>()} SET ";

                foreach (var property in GenericExtensions.GetProperties<TEntity>(excludeKey: true))
                {
                    var columnAttr = property.GetCustomAttribute<ColumnAttribute>();

                    sql += $"{columnAttr?.Name ?? property.Name} = @{property.Name},";
                }

                sql = sql.Remove(sql.Length - 1) + " ";

                sql += @$"OUTPUT INSERTED.* 
                          WHERE {GenericExtensions.GetKeyColumnName<TEntity>()} = @{GenericExtensions.GetKeyPropertyName<TEntity>()}";

                return await _dbConnection.QuerySingleAsync<TEntity>(sql, entity);
            }
            catch (Exception) { throw; }
        }

        public async Task DeleteAsync(TPrimaryKeyType entityId)
        {
            try
            {
                string sql = @$"DELETE FROM {GenericExtensions.GetTableName<TEntity>()} 
                                WHERE {GenericExtensions.GetKeyColumnName<TEntity>()} = @EntityId";

                await _dbConnection.ExecuteScalarAsync(sql, new { EntityId = entityId });
            }
            catch (Exception) { throw; }
        }

        #region Additional Functionality

        protected void TrySetCreatedOnDate(object entity)
        {
            var createdOnProperty = entity.GetType().GetProperty("CreatedOn");

            if (createdOnProperty == null)
                return;

            createdOnProperty.SetValue(entity, DateTimeOffset.UtcNow);
        }

        protected void TrySetUpdatedOnDate(object entity)
        {
            var updatedOnProperty = entity.GetType().GetProperty("UpdatedOn");

            if (updatedOnProperty == null)
                return;

            updatedOnProperty.SetValue(entity, DateTimeOffset.UtcNow);
        }

        protected void TrySetCreatedBy(object entity)
        {
            var createdByProperty = entity.GetType().GetProperty("CreatedBy");

            if (createdByProperty == null)
                return;

            createdByProperty.SetValue(entity, _userContext.CurrentUserIdentifier);
        }

        protected void TrySetUpdatedBy(object entity)
        {
            var updatedByProperty = entity.GetType().GetProperty("UpdatedBy");

            if (updatedByProperty == null)
                return;

            updatedByProperty.SetValue(entity, _userContext.CurrentUserIdentifier);
        }

        #endregion
    }
}
