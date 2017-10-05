using System;
using System.Text;

namespace Minandra.FluentCqlBuilder.CqlStatements
{
    internal class CqlStatementBuilder : ICqlStatementBuilder
    {
        protected StringBuilder CqlStatement { get; }

        internal CqlStatementBuilder()
        {
            CqlStatement = new StringBuilder();
        }

        public string BuildCql() =>
            CqlStatement.ToString();
    }
}
