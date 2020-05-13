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
    public class CreateLocationCommandTest
    {
        private readonly Mock<IAsyncRepository<Location>> _repository;

        public CreateLocationCommandTest()
        {
            _repository = new Mock<IAsyncRepository<Location>>();
            _repository.Setup(x => x.AddAsync(It.IsAny<Location>()))
                .ReturnsAsync(new Location { Id = 1, Name = "South Store" });
        }

        [Fact]
        public async void ShouldReturnLocation()
        {
            // Arrange
            var command = new CreateLocationCommand(1, "South Store", DateTime.Now);

            // Act
            var handler = new CreateLocationCommandHandler(_repository.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async void ShouldCallAddAsyncOnce()
        {
            // Arrange
            var command = new CreateLocationCommand(1, "South Store", DateTime.Now);

            // Act
            var handler = new CreateLocationCommandHandler(_repository.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            _repository.Verify(x => x.AddAsync(It.IsAny<Location>()), Times.Once);
        }
    }
}
