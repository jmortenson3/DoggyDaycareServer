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
    public class CreateLocationCommandTest
    {
        private readonly Mock<ILocationRepository> _repository;

        public CreateLocationCommandTest()
        {
            _repository = new Mock<ILocationRepository>();
            _repository.Setup(x => x.AddAsync(It.IsAny<Location>())).ReturnsAsync(new Location { Id = "1", Name = "South Store" });
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
