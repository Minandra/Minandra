﻿using System;
using System.Linq.Expressions;

namespace CqlQueryBuilder.Builders.Contracts
{
    public interface IWhereBuilder<K, T> where T : class
    {
        K Where(Expression<Func<T, bool>> parameters);
        K WhereIn(Expression<Func<T, object>> parameter, object[] values);
    }
}