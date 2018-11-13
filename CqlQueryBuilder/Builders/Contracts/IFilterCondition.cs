using System;
using System.Linq.Expressions;

namespace CqlQueryBuilder.Builders.Contracts
{
    public interface IFilterCondition<K, T> where T : class
    {
        K AndIn(Expression<Func<T, object>> parameter, object[] values);
        K Contains(object value);
    }
}