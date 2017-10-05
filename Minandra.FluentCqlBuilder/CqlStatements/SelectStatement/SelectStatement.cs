namespace Minandra.FluentCqlBuilder.CqlStatements.SelectStatement
{
    internal class SelectStatement : CqlStatementBuilder
    {
        internal SelectStatement() =>
            CqlStatement.Append("SELECT ");
    }
}