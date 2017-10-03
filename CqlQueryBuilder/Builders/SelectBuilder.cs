using System;
using System.Linq.Expressions;
using CqlQueryBuilder.Base;
using CqlQueryBuilder.Builders.Contracts;

namespace CqlQueryBuilder.Builders
{
    public class SelectBuilder<T> : CqlStatementBase, ISelectBuilder<T>, IWhereBuilder<SelectBuilder<T>, T>,
        IAllowFiltering<SelectBuilder<T>> where T : class
    {
        public SelectBuilder(string query) : base(query)
        {

        }

        public SelectBuilder<T> Limit(int limit)
        {
            AddStatement(QueryHelper.Limit(limit));
            return new SelectBuilder<T>(GetCqlStatement());
        }

        public SelectBuilder<T> OrderByAsc(Expression<Func<T, object>> parameter)
        {
            var param = QueryCreate.GetParametersName(parameter);
            AddStatement(QueryHelper.OrderBy(param, true));
            return new SelectBuilder<T>(GetCqlStatement());
        }

        public SelectBuilder<T> OrderByDesc(Expression<Func<T, object>> parameter)
        {
            throw new NotImplementedException();
        }

        public SelectBuilder<T> Where(Expression<Func<T, bool>> parameters)
        {
            AddStatement(QueryHelper.Where(parameters));
            return new SelectBuilder<T>(GetCqlStatement());
        }

        public SelectBuilder<T> AllowFilter()
        {
            AddStatement(QueryHelper.AllowFiltering());
            return new SelectBuilder<T>(GetCqlStatement());
        }
    }
}