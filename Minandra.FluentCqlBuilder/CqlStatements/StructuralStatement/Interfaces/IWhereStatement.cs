namespace Minandra.FluentCqlBuilder.CqlStatements.StructuralStatement.Interfaces
{
    public interface IWhereStatement : ICqlStatementBuilder
    {
        IOrderByStatement Where();
    }
}