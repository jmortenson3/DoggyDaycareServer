using Core.Common;
using Core.Pets;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xunit;

namespace Core.Tests.UnitTests.Pets
{
    public class CreatePetCommandTest
    {
        private readonly Mock<IPetRepository> _repository;

        public CreatePetCommandTest()
        {
            _repository = new Mock<IPetRepository>();
        }

        [Fact]
        public async void ShouldReturnPet()
        {
            // Arrange
            var command = new CreatePetCommand
            {
                OwnerId = "123",
                Name = "Stevie",
                CreatedUtc = DateTime.Now
            };

            // Act
            var handler = new CreatePetCommandHandler(_repository.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async void ShouldCallAddAsyncOnce()
        {
            // Arrange
            var command = new CreatePetCommand
            {
                OwnerId = "123",
                Name = "Stevie",
                CreatedUtc = DateTime.Now
            };

            // Act
            var handler = new CreatePetCommandHandler(_repository.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            _repository.Verify(x => x.Add(It.IsAny<Pet>()), Times.Once);
        }
    }
}
