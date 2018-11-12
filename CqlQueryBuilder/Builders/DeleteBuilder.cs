using System;
using System.Linq.Expressions;
using CqlQueryBuilder.Base;
using CqlQueryBuilder.Builders.Contracts;

namespace CqlQueryBuilder.Builders
{
    public class DeleteBuilder<T> : CqlStatementBase, 
        IAndBuilder<DeleteBuilder<T>, T>,
        IWhereBuilder<WhereBuilder<T>, T> where T : class
    {
        public DeleteBuilder(string query) : base(query) { }

        public WhereBuilder<T> Where(Expression<Func<T, bool>> parameters)
        {
            this.AddStatement(QueryHelper.Where(parameters));
            return new WhereBuilder<T>(Build());
        }

        public WhereBuilder<T> WhereIn(Expression<Func<T, object>> parameter, object[] values)
        {
            this.AddStatement(QueryHelper.WhereIn(parameter, values));
            return new WhereBuilder<T>(Build());
        }

        public DeleteBuilder<T> And(Expression<Func<T, bool>> parameters)
        {
            this.AddStatement(QueryHelper.And(parameters));
            return new DeleteBuilder<T>(Build());
        }
    }
}