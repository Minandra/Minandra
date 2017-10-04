using System;
using System.Linq.Expressions;
using Minandra.FluentCqlBuilder.Base;
using Minandra.FluentCqlBuilder.Builders.Contracts;

namespace Minandra.FluentCqlBuilder.Builders
{
    public class DeleteBuilder<T> : CqlStatementBase, IWhereBuilder<DeleteBuilder<T>, T> where T : class
    {
        public DeleteBuilder(string query) : base(query)
        {
        }

        public DeleteBuilder<T> Where(Expression<Func<T, bool>> parameters)
        {
            this.AddStatement(QueryHelper.Where(parameters));
            return new DeleteBuilder<T>(GetCqlStatement());
        }
    }
}