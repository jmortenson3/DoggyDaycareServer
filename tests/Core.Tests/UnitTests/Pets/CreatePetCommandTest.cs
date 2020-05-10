using Core.Common;
using Core.Pets;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xunit;

namespace Core.Tests.Pets
{
    public class CreatePetCommandTest
    {
        private readonly Mock<IAsyncRepository<Pet>> _repository;

        public CreatePetCommandTest()
        {
            _repository = new Mock<IAsyncRepository<Pet>>();
            _repository.Setup(x => x.AddAsync(It.IsAny<Pet>()))
                .ReturnsAsync(new Pet { Id = 2, Name = "Stevie", OwnerId = "1" });
        }

        [Fact]
        public async void ShouldCreatePet()
        {
            // Arrange
            var pet = new Pet
            {
                Id = 2,
                Name = "Stevie",
                OwnerId = "1"
            };
            var command = new CreatePetCommand
            {
                Pet = pet
            };

            // Act
            var handler = new CreatePetCommandHandler(_repository.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
        }
    }
}
