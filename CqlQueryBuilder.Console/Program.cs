using System;
using System.Linq.Expressions;
using CqlQueryBuilder.Console.Model;

namespace CqlQueryBuilder.Console
{
    class Program
    {
        static void Main(string[] args)
        {
          var delete =  QueryBuilder.New().Delete<Person>()
                .Where(p => p.Active).IF(p => p.Id > 10)
                .Build();




            //var select = QueryBuilder.New()
            //    .Select<Person>(p => new { p.Id, p.Name, p.Genre })
            //    .Where(p => p.Id == 1)
            //    .And(p => p.Genre == "M")
            //    .Build();



            //var selectDate = QueryBuilder.New()
            //    .SelectCount<Person>()
            //    .Build();

        }
    }
}
