using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Minandra.FluentCqlBuilder.CqlStatements.StructuralStatement
{
    internal class FieldClause<T> : CqlStatementBuilder where T : class
    {
        internal List<string> columns;

        public FieldClause()
        {
            columns = new List<string>();
        }

        internal FieldClause<T> Field(Expression<Func<T, object>> column)
        {
            columns.Add((column.Body as MemberExpression).Member.Name);
            return this;
        }
    }
}