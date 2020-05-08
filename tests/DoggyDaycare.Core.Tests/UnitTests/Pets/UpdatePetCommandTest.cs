using DoggyDaycare.Core.Common;
using DoggyDaycare.Core.Pets;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xunit;

namespace DoggyDaycare.Core.Tests.Pets
{
    public class UpdatePetCommandTest
    {
        private readonly Mock<IAsyncRepository<Pet>> _repository;

        public UpdatePetCommandTest()
        {
            _repository = new Mock<IAsyncRepository<Pet>>();
            _repository.Setup(x => x.UpdateAsync(It.IsAny<Pet>()))
                .ReturnsAsync(new Pet { Id = 1, Name = "Pickles", OwnerId = "1" });
        }

        [Fact]
        public async void ShouldUpdatePet()
        {
            // Arrange
            var updatedPet = new Pet
            {
                Id = 1,
                Name = "Pickles",
                OwnerId = "1"
            };
            var command = new UpdatePetCommand
            {
                Pet = updatedPet
            };

            // Act
            var handler = new UpdatePetCommandHandler(_repository.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
        }
    }
}
