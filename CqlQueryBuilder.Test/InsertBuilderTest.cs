using CqlQueryBuilder.Test.Model;
using FluentAssertions;
using System;
using Xunit;

namespace CqlQueryBuilder.Test
{
    public class InsertBuilderTest
    {
        [Fact]
        public void InsertStatementShouldNotBeNullOrEmpty()
        {
            var product = new Product
            {
                Enabled = true,
                Id = Guid.NewGuid(),
                Name = "Product Name",
                Price = 150.00m,
                CreatedOn = DateTime.Now,
                Registered = DateTime.Now
            };

            QueryBuilder
                .New()
                .Insert(product)
                .GetCqlStatement()
                .Should()
                .NotBeNullOrWhiteSpace();
        }

        [Fact]
        public void InsertStatementShouldContain_InsertStatementParts()
        {
            var product = new Product
            {
                Enabled = true,
                Id = Guid.NewGuid(),
                Name = "Product Name",
                Price = 150.00m,
                CreatedOn = DateTime.Now,
                Registered = DateTime.Now
            };

            string insertStatement = QueryBuilder.New()
                .Insert(product)
                .GetCqlStatement();

            insertStatement
                .Should()
                .Contain("INSERT");

            insertStatement
                .Should()
                .Contain("Product");

            insertStatement
                .Should()
                .Contain("VALUES");
        }
    }
}
