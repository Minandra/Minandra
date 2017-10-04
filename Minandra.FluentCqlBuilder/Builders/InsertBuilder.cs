using Minandra.FluentCqlBuilder.Base;

namespace Minandra.FluentCqlBuilder.Builders
{
    public class InsertBuilder<T> : CqlStatementBase
    {
        public InsertBuilder(string query) : base(query)
        {
        }
    }
}