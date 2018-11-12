using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using CqlQueryBuilder.TypeConversion;

namespace CqlQueryBuilder.Base
{
    public static class QueryCreate
    {
        public static string GetParameterName<T>(Expression<Func<T, object>> field)
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

        public static string GetParameterName<T>(Expression<Func<T, object>>[] fields)
        {
            if (fields == null || !fields.Any())
            {
                const string messageSelectParameters = "Ex: Select<T>(p => new {p.Id, p.Name})";
                var msgError = $"Argument must not be null or empty. Use a criteria: {messageSelectParameters}";
                throw new ArgumentNullException(nameof(fields), msgError);
            }

            var parameters = string.Empty;

            var args = ((dynamic) fields.FirstOrDefault()?.Body)?.Arguments;

            foreach (var arg in args)
                parameters += Recursive(arg) + ", ";

            return parameters.Remove(parameters.Length - 2);
        }

        private static object GetValue(Expression member)
        {
            var objectMember = Expression.Convert(member, typeof(object));
            var lambda = Expression.Lambda<Func<object>>(objectMember);
            var result = lambda.Compile();
            return result();
        }

        private static string RecursiveBinaryExpression(Expression expression)
        {
            var body = (BinaryExpression)expression;
            var right = Recursive(body.Right);
            return Recursive(body.Left) + " " + TypeConverter.NodeTypeToString(body.NodeType) + " " + right;
        }

        private static string RecursiveConstantExpression(Expression expression)
        {
            var constant = (ConstantExpression)expression;
            return TypeConverter.ConvertToTypeCode(constant);
        }

        private static string RecursiveMemberExpression(Expression expression)
        {
            var member = (MemberExpression)expression;

            if (!(member.Member is PropertyInfo property))
                throw new Exception($"Expression does not refer to a property or field: {expression}");

            // hack DateTime
            if (property.DeclaringType == typeof(DateTime))
            {
                return TypeConverter.ConvertToTypeCode(GetValue(member));
            }

            // if true expression lambda. ex: p => p.Enabled / p => !p.Enabled
            if (property.PropertyType == typeof(bool))
            {
                var unary = Expression.Convert(expression, typeof(object));
                return $"{property.Name} = {TypeConverter.NodeTypeToString(unary.NodeType)}";
            }

            return property.Name;
        }

        private static string RecursiveUnaryExpression(Expression expression)
        {
            var unary = (UnaryExpression)expression;
            var right = Recursive(unary.Operand);
            var result = $"{right} = {TypeConverter.NodeTypeToString(unary.NodeType)}";
            return result;
        }

        private static string Recursive(Expression expression)
        {
            switch (expression)
            {
                case BinaryExpression _:
                    return RecursiveBinaryExpression(expression);
                case ConstantExpression _:
                    return RecursiveConstantExpression(expression);
                case MemberExpression _:
                    return RecursiveMemberExpression(expression);
                case UnaryExpression _:
                    return RecursiveUnaryExpression(expression);
            }

            throw new Exception($"Expression does not refer to a property or field: {expression}");
        }

        public static string ToCql<T>(Expression<Func<T, bool>> expression)
        {
            return Recursive(expression.Body);
        }
    }
}