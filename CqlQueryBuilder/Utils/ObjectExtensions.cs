using CqlQueryBuilder.TypeConversion;
using System.Collections.Generic;
using System.Linq;

namespace CqlQueryBuilder.Utils
{
    public static class ObjectExtensions
    {
        public static string GetAllValuesOfPropertiesWithComma(this object obj) =>
            string.Join(", ", obj.GetType().GetProperties()
                .Select(x => GetProperlyValue(x.GetValue(obj, null))));

        //TODO: Check if exists a ColumnName attribute mapping an object property
        public static string GetMappedPropertiesAndValuesForUpdate(this object obj)
        {
            var primaryKey = obj.GetType().GetProperties()
                .SingleOrDefault(property => property.Name.ToLower().Equals("id"));

            var propertiesWithoutPrimaryKey = obj.GetType().GetProperties()
                .Where(property => !property.Name.ToLower().Equals("Id"));

            string statement = string.Join(", ", propertiesWithoutPrimaryKey.Select(property =>
                    property.Name + " = " + GetProperlyValue(property.GetValue(obj, null))));

            return statement + $" WHERE {primaryKey.Name} = "
                + GetProperlyValue(primaryKey.GetValue(obj, null));
        }

        private static string GetProperlyValue(object value) =>
            TypeConverter.ConvertToDbType(value);

    }
}