namespace CqlQueryBuilder.Builders.Contracts
{
    public interface IAllowFiltering<out T> where T : class
    {
        T AllowFiltering();
    }
}