using System;
using System.Linq.Expressions;
using CqlQueryBuilder.Base;
using CqlQueryBuilder.Builders.Contracts;

namespace CqlQueryBuilder.Builders
{
    public class SelectBuilder<T> : CqlStatementBase, ISelectBuilder<T>,
        IWhereBuilder<WhereBuilder<T>, T> where T : class
    {
        public SelectBuilder(string query) : base(query) { }

        public SelectBuilder<T> Limit(int limit)
        {
            AddStatement(QueryHelper.Limit(limit));
            return new SelectBuilder<T>(Build());
        }

        public SelectBuilder<T> OrderByAsc(Expression<Func<T, object>> parameter)
        {
            var param = QueryCreate.GetParameterName(parameter);
            AddStatement(QueryHelper.OrderBy(param, true));
            return new SelectBuilder<T>(Build());
        }

        public SelectBuilder<T> OrderByDesc(Expression<Func<T, object>> parameter)
        {
            var param = QueryCreate.GetParameterName(parameter);
            AddStatement(QueryHelper.OrderBy(param, false));
            return new SelectBuilder<T>(Build());
        }

        public WhereBuilder<T> Where(Expression<Func<T, bool>> parameters)
        {
            AddStatement(QueryHelper.Where(parameters));
            return new WhereBuilder<T>(Build());
        }

        public WhereBuilder<T> WhereIn(Expression<Func<T, object>> parameter, object[] values)
        {
            AddStatement(QueryHelper.WhereIn(parameter, values));
            return new WhereBuilder<T>(Build());
        }
    }
}