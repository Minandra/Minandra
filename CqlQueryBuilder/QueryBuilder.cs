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

        public SelectBuilder<T> SelectCount<T>() where T : class =>
            new SelectBuilder<T>(QueryHelper.SelectCount<T>());

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
