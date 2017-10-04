using System;
using System.Linq.Expressions;
using Minandra.FluentCqlBuilder.Base;
using Minandra.FluentCqlBuilder.Builders;

namespace Minandra.FluentCqlBuilder
{
    public class QueryBuilder
    {
        private QueryBuilder() { }

        public static QueryBuilder New() =>
            new QueryBuilder();

        public SelectBuilder<T> Select<T>() where T : class =>
            new SelectBuilder<T>(QueryHelper.Select<T>());

        public SelectBuilder<T> Select<T>(params Expression<Func<T, object>>[] parameters)
            where T : class =>
                throw new NotImplementedException();

        public DeleteBuilder<T> Delete<T>() where T : class =>
            new DeleteBuilder<T>(QueryHelper.GenerateDeleteStatement<T>());

        public InsertBuilder<T> Insert<T>(T type) where T : class =>
            new InsertBuilder<T>(QueryHelper.GenerateInsertStatement<T>(type));

        public UpdateBuilder<T> Update<T>(T type) where T : class =>
            new UpdateBuilder<T>(QueryHelper.GenerateUpdateStatement<T>(type));
    }
}
