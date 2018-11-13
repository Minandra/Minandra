using System;
using System.Linq.Expressions;
using CqlQueryBuilder.Base;
using CqlQueryBuilder.Builders;

namespace CqlQueryBuilder
{
    public class QueryBuilder
    {
        private QueryBuilder() { }

        public static QueryBuilder New() =>
            new QueryBuilder();

        public SelectBuilder<T> Select<T>() where T : class => 
            new SelectBuilder<T>(QueryHelper.Select<T>());

        public SelectBuilder<T> Aggregate<T>(Expression<Func<T, object>> firstParameter, 
            Expression<Func<T, object>>[] parameters, bool isCount) where T : class
        {
            var p1 = QueryCreate.GetParameterName(firstParameter);
            var p2 = string.Empty;

            if (parameters != null)
                p2 = QueryCreate.GetParameterName(parameters);

            return isCount 
                ? new SelectBuilder<T>(QueryHelper.SelectCount<T>(p1, p2)) 
                : new SelectBuilder<T>(QueryHelper.SelectMax<T>(p1, p2));
        }

        public SelectBuilder<T> SelectCount<T>(Expression<Func<T, object>> countParameter, params Expression<Func<T, object>>[] parameters) where T : class
        {
            return Aggregate(countParameter, parameters, true);
        }

        public SelectBuilder<T> SelectMax<T>(Expression<Func<T, object>> maxParameter, params Expression<Func<T, object>>[] parameters) where T : class
        {
            return Aggregate(maxParameter, parameters, false);
        }

        public SelectBuilder<T> Select<T>(params Expression<Func<T, object>>[] parameters) 
            where T : class => new SelectBuilder<T>(QueryHelper.Select<T>(QueryCreate.GetParameterName(parameters)));

        public DeleteBuilder<T> Delete<T>() where T : class =>
            new DeleteBuilder<T>(QueryHelper.Delete<T>());

        public InsertBuilder<T> Insert<T>(T type) where T : class =>
            new InsertBuilder<T>(QueryHelper.GenerateInsertStatement(type));

        public UpdateBuilder<T> Update<T>(T type) where T : class =>
            new UpdateBuilder<T>(QueryHelper.GenerateUpdateStatement(type));
    }
}
