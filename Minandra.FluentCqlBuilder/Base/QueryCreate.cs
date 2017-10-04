using Minandra.FluentCqlBuilder.Utils;
using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Minandra.FluentCqlBuilder.Base
{
    public static class QueryCreate
    {
        private static string ConvertValue(object value)
        {
            var result = value.GetType().Name.ToLower();

            switch (result)
            {
                case "string":
                case "datetime":
                    value = $"'{value}'";
                    break;
            }

            return value.ToString();
        }

        public static string GetTableName<T>() => typeof(T).GetMappedTableName();

        public static string GetPropertiesName<T>() =>
            string.Join(", ", typeof(T).GetListOfMappedProperties());

        public static string GetValuesAndParametersName<T>(params Expression<Func<T, object>>[] fields)
        {
            foreach (var field in fields)
            {
                Recursive(field);
            }

            return null;
        }

        public static string GetValuesByParametersName<T>(Expression<Func<T, object>>[] fields)
        {
            var values = string.Empty;

            foreach (var field in fields)
            {
                var right = ((BinaryExpression)field.Body).Right;
                var value = Expression.Lambda(right).Compile().DynamicInvoke();
                value = ConvertValue(value);
                values += $"{value}, ";
            }

            return values.Remove(values.Length - 2);
        }

        public static string GetParametersName<T>(Expression<Func<T, object>> field)
        {
            var parameters = string.Empty;

            var body = field.Body;

            if (body is UnaryExpression unaryExp)
                body = unaryExp.Operand;

            var memberExp = body as MemberExpression;
            var member = memberExp?.Member;
            var customAttr = member?.GetCustomAttributes(typeof(ColumnAttribute), true)
                .FirstOrDefault();

            if (customAttr != null)
            {
                var attr = customAttr as ColumnAttribute;
                parameters += $"{attr?.Name}, ";
            }
            else
            {
                parameters += $"{member?.Name}, ";
            }

            return parameters.Remove(parameters.Length - 2);
        }

        public static string GetParametersName<T>(Expression<Func<T, object>>[] fields)
        {
            var parameters = string.Empty;

            foreach (var field in fields)
            {
                parameters += GetParametersName(field) + ", ";
            }

            return parameters.Remove(parameters.Length - 2);
        }

        private static string ValueToString(object value, bool isUnary)
        {
            if (!(value is bool))
                return ConvertValue(value);

            var result = Convert.ToBoolean(value);

            if (isUnary)
                return result ? "(1=1)" : "(1=0)";

            return result ? "1" : "0";
        }

        private static string NodeTypeToString(ExpressionType nodeType, bool rightIsNull)
        {
            switch (nodeType)
            {
                case ExpressionType.Add:
                    return "+";
                case ExpressionType.And:
                    return "&";
                case ExpressionType.AndAlso:
                    return "AND";
                case ExpressionType.Divide:
                    return "/";
                case ExpressionType.Equal:
                    return rightIsNull ? "IS" : "=";
                case ExpressionType.ExclusiveOr:
                    return "^";
                case ExpressionType.GreaterThan:
                    return ">";
                case ExpressionType.GreaterThanOrEqual:
                    return ">=";
                case ExpressionType.LessThan:
                    return "<";
                case ExpressionType.LessThanOrEqual:
                    return "<=";
                case ExpressionType.Modulo:
                    return "%";
                case ExpressionType.Multiply:
                    return "*";
                case ExpressionType.Negate:
                    return "-";
                case ExpressionType.Not:
                    return "NOT";
                case ExpressionType.NotEqual:
                    return "<>";
                case ExpressionType.Or:
                    return "|";
                case ExpressionType.OrElse:
                    return "OR";
                case ExpressionType.Subtract:
                    return "-";
            }

            throw new Exception($"Error: {nodeType}");
        }

        private static object GetValue(Expression member)
        {
            var objectMember = Expression.Convert(member, typeof(object));
            var lambda = Expression.Lambda<Func<object>>(objectMember);
            var result = lambda.Compile();
            return result();
        }

        private static string RecursiveUnaryExpression(Expression expression)
        {
            var unary = (UnaryExpression)expression;
            var right = Recursive(unary.Operand);
            var result = NodeTypeToString(unary.NodeType, right == "NULL") + " " + right;

            if (result.Contains("NOT") && result.Contains("IN"))
            {
                result = result.Replace("NOT", "").Trim();
                var pos = result.IndexOf("IN", StringComparison.Ordinal);
                return result.Insert(pos, "NOT ");
            }

            if (result.Contains("NOT"))
            {
                result = $"{right} = false";
            }

            return result;
        }

        private static string RecursiveBinaryExpression(Expression expression)
        {
            var body = (BinaryExpression)expression;
            var right = Recursive(body.Right);
            return Recursive(body.Left) + " " + NodeTypeToString(body.NodeType, right == "NULL") + " " + right;
        }

        private static string RecursiveConstantExpression(Expression expression)
        {
            var constant = (ConstantExpression)expression;
            return ConvertValue(constant);
        }

        private static string RecursiveMemberExpression(Expression expression)
        {
            var member = (MemberExpression)expression;

            if (member.Member is PropertyInfo property)
            {
                var attributes = property.CustomAttributes.ToList();

                if (attributes.Any())
                {
                    foreach (var attr in attributes)
                    {
                        return attr.ConstructorArguments.FirstOrDefault().Value.ToString();
                    }
                }

                // hack DateTime
                if (member.Member is FieldInfo || property.DeclaringType == typeof(DateTime))
                {
                    return ValueToString(GetValue(member), false);
                }

                return property.Name;
            }

            throw new Exception($"Expression does not refer to a property or field: {expression}");
        }

        private static string RecursiveMethodCallExpression(Expression expression)
        {
            var methodCall = (MethodCallExpression) expression;

            //// like:
            //if (methodCall.Method == typeof(string).GetMethod("Contains", new[] { typeof(string) }))
            //{
            //    return Recursive(methodCall.Object) + " LIKE '%" + Recursive(methodCall.Arguments[0]) + "%')";
            //}
            //if (methodCall.Method == typeof(string).GetMethod("StartsWith", new[] { typeof(string) }))
            //{
            //    return Recursive(methodCall.Object) + " LIKE " + Recursive(methodCall.Arguments[0]) + "%')";
            //}
            //if (methodCall.Method == typeof(string).GetMethod("EndsWith", new[] { typeof(string) }))
            //{
            //    return Recursive(methodCall.Object) + " LIKE '%" + Recursive(methodCall.Arguments[0]) + "')";
            //}

            if (methodCall.Method.Name == "Parse")
            {
                var objectMember = Expression.Convert(methodCall, typeof(object));
                var getterLambda = Expression.Lambda<Func<object>>(objectMember);

                var unaryExpression = (UnaryExpression) getterLambda.Body;
                var methodCallExpression = (MethodCallExpression) unaryExpression.Operand;

                var methodInfoExpression = (ConstantExpression) methodCallExpression.Arguments.Last();
                return methodInfoExpression.Value.ToString();
            }

            if (methodCall.Method.Name == "Contains")
            {
                Expression collection;
                Expression property;
                if (methodCall.Method.IsDefined(typeof(ExtensionAttribute)) && methodCall.Arguments.Count == 2)
                {
                    collection = methodCall.Arguments[0];
                    property = methodCall.Arguments[1];
                }
                else
                {
                    if (!methodCall.Method.IsDefined(typeof(ExtensionAttribute)) && methodCall.Arguments.Count == 1)
                    {
                        collection = methodCall.Object;
                        property = methodCall.Arguments[0];
                    }
                    else
                    {
                        throw new Exception("Error in method: " + methodCall.Method.Name);
                    }
                }

                var values = (IEnumerable)GetValue(collection);
                var concated = values.Cast<object>().Aggregate("", (current, e) => current + ValueToString(e, false) + ", ");

                if (string.IsNullOrEmpty(concated))
                {
                    return ValueToString(false, true);
                }

                return Recursive(property) + " IN (" + concated.Substring(0, concated.Length - 2) + ")";
            }

        
           return GetValue(expression).ToString();
        }

        private static string Recursive(Expression expression)
        {
            if (expression is UnaryExpression)
            {
                return RecursiveUnaryExpression(expression);
            }

            if (expression is BinaryExpression)
            {
                return RecursiveBinaryExpression(expression);
            }

            if (expression is ConstantExpression)
            {
                return RecursiveConstantExpression(expression);
            }

            if (expression is MemberExpression)
            {
                return RecursiveMemberExpression(expression);
            }

            if (expression is MethodCallExpression)
            {
                return RecursiveMethodCallExpression(expression);
            }

            throw new Exception($"Expression does not refer to a property or field: {expression}");
        }

        public static string ToCql<T>(Expression<Func<T, bool>> expression)
        {
            return Recursive(expression.Body);
        }
    }
}