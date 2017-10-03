using CqlQueryBuilder.Base;
using System;

namespace CqlQueryBuilder.Test.Model
{
    //[Table("Product")]
    public class Product
    {
        public Guid Id { get; set; }
        public DateTime Registered { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool Enabled { get; set; }
    }
}
