using CqlQueryBuilder.Base;

namespace CqlQueryBuilder.Builders
{
    public class InsertBuilder<T> : CqlStatementBase
    {
        public InsertBuilder(string query) : base(query)
        {
        }
    }
}