using System;
using System.Linq.Expressions;

namespace CqlQueryBuilder.Builders.Contracts
{
    public interface IUpdateBuilder<T> where T : class
    {
        T Set(Expression<Func<T, object>> parameter);
    }
}