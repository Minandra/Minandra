using System;
using System.Linq.Expressions;
using CqlQueryBuilder.Base;
using CqlQueryBuilder.Builders.Contracts;

namespace CqlQueryBuilder.Builders
{
    public class WhereBuilder<T> : CqlStatementBase,
        IAllowFiltering<WhereBuilder<T>>,
        IFilterCondition<WhereBuilder<T>, T>,
        IAndBuilder<WhereBuilder<T>, T> where T : class
    {
        public WhereBuilder(string query) : base(query) { }

        public WhereBuilder<T> And(Expression<Func<T, bool>> parameters)
        {
            this.AddStatement(QueryHelper.And(parameters));
            return new WhereBuilder<T>(Build());
        }
        
        public WhereBuilder<T> AndIn(Expression<Func<T, object>> parameter, object[] values)
        {
            AddStatement(QueryHelper.AndIn(parameter, values));
            return new WhereBuilder<T>(Build());
        }

        public WhereBuilder<T> Contains(object value)
        {
            AddStatement(QueryHelper.Contains(value));
            return new WhereBuilder<T>(Build());
        }

        public WhereBuilder<T> AllowFiltering()
        {
            AddStatement(QueryHelper.AllowFiltering());
            return new WhereBuilder<T>(Build());
        }
    }
}