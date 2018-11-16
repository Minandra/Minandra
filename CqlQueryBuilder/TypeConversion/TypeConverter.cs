using System;
using System.Globalization;
using System.Linq.Expressions;

namespace CqlQueryBuilder.TypeConversion
{
    public class TypeConverter
    {
        public static string ConvertToTypeCode(object value)
        {
            //switch (Type.GetTypeCode(value.GetType()))
            //{
            //    case TypeCode.Byte:
            //    case TypeCode.Empty:
            //    case TypeCode.DBNull:
            //    case TypeCode.Object:
            //        return string.Empty;

            //    case TypeCode.Boolean:
            //        return value.ToString();
            //    case TypeCode.DateTime:
            //        return $"'{Convert.ToDateTime(value, CultureInfo.InvariantCulture)}'";
            //    case TypeCode.Decimal:
            //    case TypeCode.Double:
            //    case TypeCode.Single:
            //        return $"{Convert.ToDecimal(value).ToString("F3", CultureInfo.InvariantCulture)}";
            //    case TypeCode.UInt16:
            //    case TypeCode.UInt32:
            //    case TypeCode.UInt64:
            //    case TypeCode.SByte:
            //    case TypeCode.Int16:
            //    case TypeCode.Int32:
            //    case TypeCode.Int64:
            //        return value.ToString();
            //    case TypeCode.String:
            //    case TypeCode.Char:
            //        return $"'{value.ToString().Replace("\"", "'")}'";
            //    default:
            //        return string.Empty;
            //}

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