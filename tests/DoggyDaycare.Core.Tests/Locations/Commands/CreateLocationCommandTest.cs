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
    public class CreateLocationCommandTest
    {
        private readonly Mock<ILocationRepository> _repository;

        public CreateLocationCommandTest()
        {
            _repository = new Mock<ILocationRepository>();
        }

        [Fact]
        public async void ShouldCreateLocation()
        {
            // Arrange
            var location = new Location
            {
                Id = "1",
                Name = "South Store"
            };
            var command = new CreateLocationCommand
            {
                Location = location
            };

            // Act
            var handler = new CreateLocationCommandHandler(_repository.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
        }
    }
}
