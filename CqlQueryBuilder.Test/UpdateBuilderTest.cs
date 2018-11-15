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
            // Arrange
            var createdOn = DateTime.Now;
            var registered = DateTime.Now;
            var id = Guid.Parse("15eb2128-2f2b-49c5-8bc8-5cbc4c75132d");

            var expectedCqlUpdateStatement = $"UPDATE Product SET Id = '{id}', Registered = '{registered}', CreatedOn = '{createdOn}', Name = 'Product Name', Price = 150.000, Enabled = True WHERE Id = '{id}'";

            var product = new Product
            {
                Enabled = true,
                Id = id,
                Name = "Product Name",
                Price = 150.00m,
                CreatedOn = createdOn,
                Registered = registered
            };

            // Act

            string cqlUpdateStatementOutcome =
                QueryBuilder
                .New()
                .Update(product)
                .Build();

            // Assert

            cqlUpdateStatementOutcome
                .Should()
                .NotBeNullOrWhiteSpace()
                    .And
                .Be(expectedCqlUpdateStatement, $"The CQL Update statement must be equals to the expected one.");
        }
    }
}
