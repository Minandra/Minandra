using System;
using System.Linq.Expressions;
using CqlQueryBuilder.Base;
using CqlQueryBuilder.Builders.Contracts;

namespace CqlQueryBuilder.Builders
{
    public class WhereBuilder<T> : CqlStatementBase,
        IAllowFiltering<WhereBuilder<T>>,
        IWhereBuilderExtension<WhereBuilder<T>, T>,
        IDeleteBuilderExtension<DeleteBuilder<T>, T>,
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

        public WhereBuilder<T> ContainsKey(object value)
        {
            AddStatement(QueryHelper.Contains(value, true));
            return new WhereBuilder<T>(Build());
        }

        public WhereBuilder<T> AllowFiltering()
        {
            AddStatement(QueryHelper.AllowFiltering());
            return new WhereBuilder<T>(Build());
        }

        public DeleteBuilder<T> IF(Expression<Func<T, bool>> parameter)
        {
            this.AddStatement(QueryHelper.IF(parameter));
            return new DeleteBuilder<T>(Build());
        }

        public DeleteBuilder<T> IFExists()
        {
            this.AddStatement(QueryHelper.IFExists());
            return new DeleteBuilder<T>(Build());
        }
    }
}