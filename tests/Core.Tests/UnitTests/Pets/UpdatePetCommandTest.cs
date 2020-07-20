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
    public class UpdatePetCommandTest
    {
        private readonly Mock<IPetRepository> _repository;

        public UpdatePetCommandTest()
        {
            _repository = new Mock<IPetRepository>();
            _repository.Setup(x => x.Update(It.IsAny<Pet>()))
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
