using CqlQueryBuilder.Test.Model;
using FluentAssertions;
using System;
using Xunit;

namespace CqlQueryBuilder.Test
{
    public class InsertBuilderTest
    {
        [Fact]
        public void QueryBuilder_GeneratesInsertStatement_ShouldNotBeNullOrEmpty()
        {
            // Arrange
            var createdOn = DateTime.Now;
            var registered = DateTime.Now;
            var id = Guid.Parse("15eb2128-2f2b-49c5-8bc8-5cbc4c75132d");

            var expectedInsertCQLStatement = $"INSERT INTO Product (Id, Registered, CreatedOn, Name, Price, Enabled) VALUES ('{id}', '{createdOn}', '{registered}', 'Product Name', 150.000, True)";

            var product = new Product
            {
                Enabled = true,
                Id = id,
                Name = "Product Name",
                Price = 150.000m,
                CreatedOn = createdOn,
                Registered = registered
            };

            // Act

            string cqlInsertStatementOutcome = QueryBuilder
                .New()
                .Insert(product)
                .Build();

            // Assert

            cqlInsertStatementOutcome
                .Should()
                .NotBeNullOrWhiteSpace()
                    .And
                .Be(expectedInsertCQLStatement, $"The CQL Insert statement must be equals to the expected one.");
        }
    }
}
