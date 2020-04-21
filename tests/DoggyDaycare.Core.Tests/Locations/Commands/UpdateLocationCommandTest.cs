using DoggyDaycare.Core.Common;
using DoggyDaycare.Core.Locations.Commands;
using DoggyDaycare.Core.Locations.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xunit;

namespace DoggyDaycare.Core.Tests.Locations.Commands
{
    public class UpdateLocationCommandTest
    {
        private readonly Mock<ILocationRepository> _repository;

        public UpdateLocationCommandTest()
        {
            _repository = new Mock<ILocationRepository>();
        }

        [Fact]
        public async void ShouldUpdateLocation()
        {
            // Arrange
            var updatedLocation = new Location
            {
                Id = "1",
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
