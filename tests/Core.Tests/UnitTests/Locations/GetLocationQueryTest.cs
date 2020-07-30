using Core.Common;
using Core.Locations;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xunit;

namespace Core.Tests.UnitTests.Locations
{
    public class GetLocationQueryTest
    {
        private readonly Mock<ILocationRepository> _repository;

        public GetLocationQueryTest()
        {
            _repository = new Mock<ILocationRepository>();
            _repository.Setup(x => x.FindAsync(It.Is<int>(val => val == 1)))
                .ReturnsAsync(new Location { Id = 1, Name = "South Store" });
        }

        [Fact]
        public async void ShouldReturnLocation()
        {
            // Arrange
            var query = new GetLocationQuery { Id = 1 };

            // Act
            var handler = new GetLocationQueryHandler(_repository.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);

        }

        [Fact]
        public async void ShouldCallFindAsyncOnce()
        {
            // Arrange
            var query = new GetLocationQuery { Id = 1 };

            // Act
            var handler = new GetLocationQueryHandler(_repository.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            _repository.Verify(x => x.FindAsync(1), Times.Once);

        }
    }
}
