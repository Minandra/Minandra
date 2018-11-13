using System;
using System.Linq.Expressions;

namespace CqlQueryBuilder.Builders.Contracts
{
    public interface IAndBuilder<K, T> where T : class
    {
        K And(Expression<Func<T, bool>> parameters);
    }
}