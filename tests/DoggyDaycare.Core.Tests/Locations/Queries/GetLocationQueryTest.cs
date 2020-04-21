using DoggyDaycare.Core.Common;
using DoggyDaycare.Core.Locations.Entities;
using DoggyDaycare.Core.Locations.Queries;
using DoggyDaycare.Infrastructure;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xunit;

namespace DoggyDaycare.Core.Tests.Locations.Queries
{
    public class GetLocationQueryTest
    {
        private readonly Mock<ILocationRepository> _repository;

        public GetLocationQueryTest()
        {
            _repository = new Mock<ILocationRepository>();
        }

        [Fact]
        public async void ShouldReturnLocation()
        {
            // Arrange
            var expected = new Location
            {
                Id = "1",
                Name = "South Store"
            };
            var query = new GetLocationQuery
            {
                Id = "1"
            };

            // Act
            var handler = new GetLocationQueryHandler(_repository.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Equal(expected.Id, result.Id);
            Assert.Equal(expected.Name, result.Name);

        }

        [Fact]
        public async void ShouldReturnNullLocation()
        {
            // Arrange
            var query = new GetLocationQuery
            {
                Id = "-1"
            };

            // Act
            var handler = new GetLocationQueryHandler(_repository.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Null(result);
        }
    }
}
