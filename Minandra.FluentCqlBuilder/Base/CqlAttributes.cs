using System;

namespace Minandra.FluentCqlBuilder.Base
{

    [AttributeUsage(AttributeTargets.Class)]
    public class TableAttribute : Attribute
    {
        public string Name { get; set; }
        public TableAttribute(string name)
        {
            Name = name;
        }
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class ColumnAttribute : Attribute
    {
        public string Name { get; set; }
        public bool IsPrimaryKey { get; set; }

        public ColumnAttribute(string name, bool isPrimaryKey)
        {
            Name = name;
            IsPrimaryKey = isPrimaryKey;
        }
    }
}