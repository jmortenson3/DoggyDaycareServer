using DoggyDaycare.Core.Common;
using DoggyDaycare.Core.Locations;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xunit;

namespace DoggyDaycare.Core.Tests.Locations
{
    public class UpdateLocationCommandTest
    {
        private readonly Mock<IAsyncRepository<Location>> _repository;

        public UpdateLocationCommandTest()
        {
            _repository = new Mock<IAsyncRepository<Location>>();
            _repository.Setup(x => x.UpdateAsync(It.IsAny<Location>()))
                .ReturnsAsync(new Location { Id = 1, Name = "A new name" });
        }

        [Fact]
        public async void ShouldUpdateLocation()
        {
            // Arrange
            var updatedLocation = new Location
            {
                Id = 1,
                Name = "A new name"
            };

            var command = new UpdateLocationCommand
            {
                Location = updatedLocation
            };

            // Act
            var handler = new UpdateLocationCommandHandler(_repository.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
        }
    }
}
