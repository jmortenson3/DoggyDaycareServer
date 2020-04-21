using DoggyDaycare.Core.Common;
using DoggyDaycare.Core.Pets.Entities;
using DoggyDaycare.Core.Pets.Queries;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xunit;

namespace DoggyDaycare.Core.Tests.Pets.Queries
{
    public class GetPetQueryTest
    {
        private readonly Mock<IPetRepository> _repository;

        public GetPetQueryTest()
        {
            _repository = new Mock<IPetRepository>();
        }

        [Fact]
        public async void ShouldReturnPet()
        {
            // Arrange
            var expected = new Pet
            {
                Id = "1",
                Name = "Larry",
                CustomerId = "1"
            };
            var query = new GetPetQuery
            {
                Id = expected.Id
            };

            // Act
            var handler = new GetPetQueryHandler(_repository.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Equal(expected.Id, result.Id);
            Assert.Equal(expected.Name, result.Name);
            Assert.Equal(expected.CustomerId, result.CustomerId);
        }
    }
}
