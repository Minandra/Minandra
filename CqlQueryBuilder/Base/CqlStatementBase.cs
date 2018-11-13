using System.Text;

namespace CqlQueryBuilder.Base
{
    public class CqlStatementBase : IQuery
    {
        private readonly StringBuilder _cqlStatements;

        protected CqlStatementBase()
        {
            _cqlStatements = new StringBuilder();
        }

        protected CqlStatementBase(string query) : this()
        {
            _cqlStatements.Append(query);
        }

        public string Build()
        {
            return _cqlStatements.ToString();
        }

        internal CqlStatementBase AddStatement(string statement)
        {
            _cqlStatements.Append(statement);
            return this;
        }
    }
}