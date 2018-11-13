using System;
using System.Linq.Expressions;

namespace CqlQueryBuilder.TypeConversion
{
    public class TypeConverter
    {
        private static TypeCode TryGetTypeCode(dynamic value)
        {
            try
            {
                return Type.GetTypeCode(value.Type as Type);
            }
            catch
            {
                return Type.GetTypeCode(value.GetType());
            }
        }

        public static string ConvertToTypeCode(dynamic value)
        {
            var typeValue = TryGetTypeCode(value);
            var result = string.Empty;

            switch (typeValue)
            {
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                    result = value.ToString();
                    break;

                case TypeCode.String:
                    result = value.ToString().Replace("\"", "'");
                    break;

                case TypeCode.Decimal:
                case TypeCode.Single:
                    result = $"{Convert.ToDecimal(value.ToString()).ToString().Replace(",", ".")}";
                    break;

                case TypeCode.DateTime:
                    result = $"'{Convert.ToDateTime(value.ToString()):yyyy-MM-dd HH:mm:ss}'";
                    break;

                case TypeCode.Boolean:
                    result = value.ToString();
                    break;
            }

            return result;
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

            throw new Exception($"Error: {nodeType}");
        }
    }
}