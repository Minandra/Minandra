using System;
using System.Globalization;
using System.Linq.Expressions;
using System.Text;

namespace CqlQueryBuilder.TypeConversion
{
    public class TypeConverter
    {
        public static string ConvertToTypeCode(object value)
        {
            switch (value.GetType().FullName)
            {
                case "System.Int16":
                case "System.Int32":
                case "System.Int64":
                    return value.ToString();
                case "System.String":
                case "System.Guid":
                case "System.Char":
                    return $"'{value.ToString().Replace("\"", "'")}'";
                case "System.Decimal":
                case "System.Float":
                    return $"{Convert.ToDecimal(value).ToString("F3", CultureInfo.InvariantCulture)}";
                case "System.DateTimeOffset":
                    return $"'{Convert.ToDateTime(((DateTimeOffset)value).UtcDateTime, CultureInfo.InvariantCulture)}'";
                case "System.DateTime":
                    return $"'{Convert.ToDateTime(value, CultureInfo.InvariantCulture)}'";
                case "System.Boolean":
                    return value.ToString();
                default:
                    return string.Empty;
            }
        }

        //Operations: = | < | > | <= | >= | CONTAINS | CONTAINS KEY
        public static string NodeTypeToString(ExpressionType nodeType)
        {
            switch (nodeType)
            {
                case ExpressionType.And:
                case ExpressionType.AndAlso:
                    return "AND";
                case ExpressionType.Equal:
                    return "=";
                case ExpressionType.GreaterThan:
                    return ">";
                case ExpressionType.GreaterThanOrEqual:
                    return ">=";
                case ExpressionType.LessThan:
                    return "<";
                case ExpressionType.LessThanOrEqual:
                    return "<=";
                case ExpressionType.NotEqual:
                    return "<>";
                case ExpressionType.Not:
                    return "false";
                case ExpressionType.Convert: // lambda expression 
                    return "true";
            }

            throw new Exception($"The {nameof(nodeType)} with value {nodeType} is not supported.");
        }
    }
}