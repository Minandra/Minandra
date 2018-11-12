using System;
using System.Linq.Expressions;
using CqlQueryBuilder.Base;
using CqlQueryBuilder.Builders.Contracts;

namespace CqlQueryBuilder.Builders
{
    public class UpdateBuilder<T> : CqlStatementBase,
        IWhereBuilder<WhereBuilder<T>, T>,
        IUpdateBuilder<UpdateBuilder<T>> where T : class
    {
        public UpdateBuilder(string query) : base(query) { }

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

        public UpdateBuilder<T> Set(Expression<Func<UpdateBuilder<T>, object>> parameter)
        {
            throw new NotImplementedException();
        }
    }
}