using System;

namespace CqlQueryBuilder.TypeConversion
{
    public class TypeConverter
    {
        public static string ConvertToDbType(object value)
        {
            switch (value.GetType().Name.ToLower())
            {
                case "string":
                case "datetime":
                case "datetimeoffset":
                    return $"'{value}'";
                case "decimal":
                case "float":
                case "double":
                    return $"{Convert.ToDecimal(value).ToString().Replace(",", ".")}";
                case "bool":
                case "boolean":
                    return $"{(Convert.ToBoolean(value) == true ? "true" : "false")}";
            }

            return value.ToString();
        }
    }
}