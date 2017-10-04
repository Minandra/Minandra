namespace Minandra.FluentCqlMapper.MapConfiguration.Interfaces
{
    public interface IEntityPropertyDefinition
    {
        string DbColumnName { get; set; }

        bool Ignore { get; set; }
    }
}