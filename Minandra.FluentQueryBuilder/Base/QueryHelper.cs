using CqlQueryBuilder.Utils;
using System;
using System.Linq.Expressions;
using System.Text;

namespace CqlQueryBuilder.Base
{
    internal static class QueryHelper
    {
        internal static string Select<T>()
        {
            var table = QueryCreate.GetTableName<T>();
            return $"SELECT * FROM {table}";
        }

        internal static string GenerateInsertStatement<T>(T type) =>
            new StringBuilder()
                .Append("INSERT INTO ")
                .Append(typeof(T).GetMappedTableName())
                .Append($" ({typeof(T).GetListOfMappedPropertiesWithComma()}) ")
                .Append("VALUES ")
                .Append("(" + type.GetAllValuesOfPropertiesWithComma() + ")")
                .ToString();

        internal static string GenerateUpdateStatement<T>(T type) where T : class =>
            new StringBuilder()
                .Append($"UPDATE {typeof(T).GetMappedTableName()}")
                .Append(" SET ")
                .Append($" {type.GetMappedPropertiesAndValuesForUpdate()} ")
                .ToString();

        internal static string GenerateDeleteStatement<T>() =>
            new StringBuilder()
                .Append($"DELETE FROM {typeof(T).GetMappedTableName()}")
                .ToString();

        internal static string Update<T>()
        {
            var table = QueryCreate.GetTableName<T>();
            return $"UPDATE {table}";
        }

        internal static string Limit(int limit)
        {
            return $"LIMIT {limit}";
        }

        internal static string AllowFiltering()
        {
            return "ALLOW FILTERING";
        }

        internal static string OrderBy(object parameter, bool asc)
        {
            var orderby = $"ORDER BY {parameter} ";
            return asc ? orderby + "ASC" : orderby + "DESC";
        }

        internal static string Where<T>(Expression<Func<T, bool>> parameters)
        {
            var query = QueryCreate.ToCql<T>(parameters);
            return $"WHERE {query}";
        }

        internal static string Set<T>(params Expression<Func<T, object>>[] parameters)
        {
            return null;
        }
    }
}