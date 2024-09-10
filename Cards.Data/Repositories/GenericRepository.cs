﻿using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Data
{
    public class GenericRepository<TEntity, TPrimaryKeyType> : Abstractions.IGenericRepository<TEntity, TPrimaryKeyType> where TEntity : class
    {
        private readonly IDbConnection _dbConnection;

        public GenericRepository(Abstractions.IDataContext dataContext)
        {
            _dbConnection = dataContext.CreateConnection();
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
                string sql = @$"UPDATE {GenericExtensions.GetTableName<TEntity>()} SET ";

                foreach (var property in GenericExtensions.GetProperties<TEntity>(excludeKey: true))
                {
                    var columnAttr = property.GetCustomAttribute<ColumnAttribute>();

                    sql += ($"{columnAttr?.Name ?? property.Name} = @{property.Name},");
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
    }
}