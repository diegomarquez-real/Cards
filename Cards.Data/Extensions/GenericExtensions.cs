using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using PetaPoco;

namespace Cards.Data
{
    public static class GenericExtensions
    {
        public static string GetTableName<TEntity>()
        {
            var type = typeof(TEntity);
            var tableAttr = type.GetCustomAttribute<TableNameAttribute>();

            return tableAttr == null ? type.Name : tableAttr.Value;
        }

        public static string? GetKeyColumnName<TEntity>()
        {
            var pimaryKeyProperty = typeof(TEntity).GetCustomAttribute<PrimaryKeyAttribute>();

            if (pimaryKeyProperty == null)
                return null;

            return pimaryKeyProperty.Value;
        }


        public static string GetColumns<TEntity>(bool excludeKey = false)
        {
            var type = typeof(TEntity);
            var columns = string.Join(", ", type.GetProperties()
                          .Where(p => !excludeKey || !p.IsDefined(typeof(System.ComponentModel.DataAnnotations.KeyAttribute)))
                          .Select(p =>
                          {
                              var columnAttr = p.GetCustomAttribute<ColumnAttribute>();

                              return columnAttr != null && !String.IsNullOrEmpty(columnAttr.Name) ? columnAttr.Name : p.Name;
                          }));

            return columns;
        }

        public static string GetPropertyNames<TEntity>(bool excludeKey = false)
        {
            var properties = typeof(TEntity).GetProperties().Where(p => !excludeKey || p.GetCustomAttribute<System.ComponentModel.DataAnnotations.KeyAttribute>() == null);
            var values = String.Join(", ", properties.Select(p =>
            {
                return $"@{p.Name}";
            }));

            return values;
        }

        public static IEnumerable<PropertyInfo> GetProperties<TEntity>(bool excludeKey = false)
        {
            var properties = typeof(TEntity).GetProperties().Where(p => !excludeKey || p.GetCustomAttribute<System.ComponentModel.DataAnnotations.KeyAttribute>() == null);

            return properties;
        }

        public static string? GetKeyPropertyName<TEntity>()
        {
            var pimaryKeyProperty = typeof(TEntity).GetProperties().FirstOrDefault(p => p.GetCustomAttribute<System.ComponentModel.DataAnnotations.KeyAttribute>() != null);

            if (pimaryKeyProperty == null)
                return null;

            return pimaryKeyProperty.Name;
        }
    }
}
