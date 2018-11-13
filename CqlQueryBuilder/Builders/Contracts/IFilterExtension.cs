using System;
using System.Linq.Expressions;

namespace CqlQueryBuilder.Builders.Contracts
{
    public interface IWhereBuilderExtension<K, T> where T : class
    {
        K AndIn(Expression<Func<T, object>> parameter, object[] values);
        K Contains(object value);
        K ContainsKey(object value);
    }

    public interface IDeleteBuilderExtension<K, T> where T : class
    {
        K IF(Expression<Func<T, bool>> parameter);
        K IFExists();
    }
}