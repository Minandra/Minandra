using System;
using System.Linq.Expressions;

namespace CqlQueryBuilder.Builders.Contracts
{
    public interface ISelectBuilder<T> where T : class
    {
        SelectBuilder<T> Limit(int limit);
        SelectBuilder<T> OrderByAsc(Expression<Func<T, object>> parameter);
        SelectBuilder<T> OrderByDesc(Expression<Func<T, object>> parameter);
    }
}