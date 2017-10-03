using CqlQueryBuilder.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CqlQueryBuilder.Utils
{
    public static class TypeExtensions
    {
        public static string GetMappedTableName(this Type type) =>
            type.GetCustomAttributes<TableAttribute>()
                .FirstOrDefault()?.Name ?? type.Name;

        //TODO: Check if exists a ColumnName attribute mapping an object property
        public static IReadOnlyList<string> GetListOfMappedProperties(this Type type) =>
            type.GetProperties()
                .Select(x => x.Name).ToList();

        //TODO: Check if exists a ColumnName attribute mapping an object property
        public static string GetListOfMappedPropertiesWithComma(this Type type) =>
            string.Join(", ", type.GetProperties().Select(x => x.Name));
    }
}
