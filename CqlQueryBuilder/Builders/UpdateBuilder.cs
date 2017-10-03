using System;
using System.Linq.Expressions;
using CqlQueryBuilder.Base;
using CqlQueryBuilder.Builders.Contracts;

namespace CqlQueryBuilder.Builders
{
    public class UpdateBuilder<T> : CqlStatementBase, IWhereBuilder<UpdateBuilder<T>, T> where T : class
    {
        public UpdateBuilder(string query) : base(query)
        {

        }

        public UpdateBuilder<T> Set(params Expression<Func<T, object>>[] values)
        {
            var aa = QueryHelper.Set(values);
            throw new NotImplementedException();
        }

        public UpdateBuilder<T> Where(Expression<Func<T, bool>> parameters)
        {
            this.AddStatement(QueryHelper.Where(parameters));
            return new UpdateBuilder<T>(GetCqlStatement());
        }
    }
}