using System;
using System.Linq.Expressions;

namespace Minandra.FluentCqlBuilder.Builders.Contracts
{
    public interface IWhereBuilder<K, T> where T : class
    {
        K Where(Expression<Func<T, bool>> parameters);
    }
}