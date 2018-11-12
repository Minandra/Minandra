using System;

namespace CqlQueryBuilder.Console.Model
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Birth { get; set; }
        public string Genre { get; set; }
        public bool Active { get; set; }
    }
}