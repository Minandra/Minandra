using CqlQueryBuilder.Test.Model;
using FluentAssertions;
using System;
using Xunit;

namespace CqlQueryBuilder.Test
{
    public class UpdateBuilderTest
    {
        [Fact]
        public void UpdateStatementShouldNotBeNullOrEmptyForWholeInstance()
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
                .Update(product)
                .GetCqlStatement()
                .Should()
                .NotBeNullOrWhiteSpace();
        }

        [Fact]
        public void UpdateStatementShouldContain_UpdateStatementPartsForWholeInstance()
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

            string updateStatement = QueryBuilder
                .New()
                .Update(product)
                .GetCqlStatement();

            updateStatement
                .Should()
                .Contain("UPDATE");

            updateStatement
                .Should()
                .Contain("SET");

            updateStatement
                .Should()
                .Contain("WHERE");
        }
    }
}
