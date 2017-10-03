using System.Text;

namespace CqlQueryBuilder.Base
{
    public class CqlStatementBase : IQuery
    {
        private StringBuilder cqlStatements;

        protected CqlStatementBase()
        {
            cqlStatements = new StringBuilder();
        }

        protected CqlStatementBase(string query)
            : this()
        {
            cqlStatements.Append(query);
        }

        public string GetCqlStatement()
        {
            return cqlStatements.ToString();
        }

        internal CqlStatementBase AddStatement(string statement)
        {
            cqlStatements.Append(statement);
            return this;
        }
    }
}