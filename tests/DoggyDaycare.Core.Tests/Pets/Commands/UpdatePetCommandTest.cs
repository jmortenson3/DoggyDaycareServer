using DoggyDaycare.Core.Common;
using DoggyDaycare.Core.Pets.Commands;
using DoggyDaycare.Core.Pets.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xunit;

namespace DoggyDaycare.Core.Tests.Pets.Commands
{
    public class UpdatePetCommandTest
    {
        private readonly Mock<IPetRepository> _repository;

        public UpdatePetCommandTest()
        {
            _repository = new Mock<IPetRepository>();
        }

        [Fact]
        public async void ShouldUpdatePet()
        {
            // Arrange
            var updatedPet = new Pet
            {
                Id = "1",
                Name = "Pickles",
                CustomerId = "1"
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
