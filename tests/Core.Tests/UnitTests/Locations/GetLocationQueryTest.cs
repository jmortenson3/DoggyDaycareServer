using Core.Common;
using Core.Locations;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xunit;

namespace Core.Tests.Locations
{
    public class GetLocationQueryTest
    {
        private readonly Mock<IAsyncRepository<Location>> _repository;

        public GetLocationQueryTest()
        {
            _repository = new Mock<IAsyncRepository<Location>>();
            _repository.Setup(x => x.FindAsync(It.Is<int>(val => val == 1)))
                .ReturnsAsync(new Location { Id = 1, Name = "South Store" });
        }

        [Fact]
        public async void ShouldReturnLocation()
        {
            // Arrange
            var expected = new Location
            {
                Id = 1,
                Name = "South Store"
            };
            var query = new GetLocationQuery
            {
                Id = 1
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
                Id = -1
            };

            // Act
            var handler = new GetLocationQueryHandler(_repository.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Null(result);
        }
    }
}
