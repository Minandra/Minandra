namespace CqlQueryBuilder.Base
{
    public interface IQuery
    {
        string GetCqlStatement();
    }
}