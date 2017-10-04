namespace Minandra.FluentCqlBuilder.Builders.Contracts
{
    public interface IAllowFiltering<out T> where T : class
    {
        T AllowFilter();
    }
}