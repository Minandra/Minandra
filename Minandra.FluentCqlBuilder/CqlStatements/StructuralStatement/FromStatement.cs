namespace Minandra.FluentCqlBuilder.CqlStatements.StructuralStatement
{
    internal class FromStatement : CqlStatementBuilder
    {
        internal FromStatement() =>
            CqlStatement.Append(" FROM ");
    }
}