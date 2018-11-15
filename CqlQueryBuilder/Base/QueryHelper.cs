using CqlQueryBuilder.Utils;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using CqlQueryBuilder.TypeConversion;

namespace CqlQueryBuilder.Base
{
    internal static class QueryHelper
    {
        internal static string Select<T>(string parameters = null)
        {
            var par = !string.IsNullOrWhiteSpace(parameters) ? parameters : "*";
            return $"SELECT {par} FROM {typeof(T).GetMappedTableName()}";
        }

        internal static string SelectCount<T>(string countParameter, string parameters)
        {
            var sqlCount = $"SELECT COUNT({countParameter})";

            if (!string.IsNullOrEmpty(parameters))
                sqlCount += $", {parameters}";

            return $"{sqlCount} FROM {typeof(T).GetMappedTableName()}";
        }

        internal static string SelectMax<T>(string countParameter, string parameters)
        {
            var sqlCount = $"SELECT MAX({countParameter})";

            if (!string.IsNullOrEmpty(parameters))
                sqlCount += $", {parameters}";

            return $"{sqlCount} FROM {typeof(T).GetMappedTableName()}";
        }

        internal static string GenerateInsertStatement<T>(T type) =>
            new StringBuilder()
                .Append("INSERT INTO ")
                .Append(typeof(T).GetMappedTableName())
                .Append($" ({typeof(T).GetListOfMappedPropertiesWithComma()}) ")
                .Append("VALUES ")
                .Append($"({type.GetAllValuesOfPropertiesWithComma()})")
                .ToString();

        internal static string GenerateUpdateStatement<T>(T type) where T : class =>
            new StringBuilder() 
                .Append($"UPDATE {typeof(T).GetMappedTableName()} SET")
                .Append($" {type.GetMappedPropertiesAndValuesForUpdate()}")
                .ToString();

        internal static string Delete<T>()
        {
            return $"DELETE FROM {typeof(T).GetMappedTableName()}";
        }

        internal static string Update<T>()
        {
            return $"UPDATE {typeof(T).GetMappedTableName()}";
        }

        internal static string Limit(int limit)
        {
            return $" LIMIT {limit}";
        }

        internal static string AllowFiltering()
        {
            return " ALLOW FILTERING";
        }

        internal static string OrderBy(object parameter, bool asc)
        {
            var orderby = $" ORDER BY {parameter} ";
            return asc ? orderby + "ASC" : orderby + "DESC";
        }

        internal static string Where<T>(Expression<Func<T, bool>> parameters)
        {
            var query = QueryCreate.ToCql(parameters);
            return $" WHERE {query}";
        }

        internal static string WhereIn<T>(Expression<Func<T, object>> parameter, object[] values)
        {
            var param = QueryCreate.GetParameterName(parameter);
            return $" WHERE {param} IN ({string.Join(", ", values.Select(p => p.GetPropertyValue()))})";
        }

        internal static string And<T>(Expression<Func<T, bool>> parameter)
        {
            var query = QueryCreate.ToCql(parameter);
            return $" AND {query}";
        }

        internal static string AndIn<T>(Expression<Func<T, object>> parameter, object[] values)
        {
            var param = QueryCreate.GetParameterName(parameter);
            return $" AND {param} IN ({string.Join(", ", values.Select(p => p.GetPropertyValue()))})";
        }

        internal static string Contains(object value, bool isKey = false)
        {
            return isKey ? 
                $" CONTAINS KEY {TypeConverter.ConvertToTypeCode(value)}" 
                : $" CONTAINS {TypeConverter.ConvertToTypeCode(value)}";
        }

        public static string IFExists()
        {
            return " IF EXISTS";
        }

        internal static string IF<T>(Expression<Func<T, bool>> parameter)
        {
            var query = QueryCreate.ToCql(parameter);
            return $" IF {query}";
        }
    }
}