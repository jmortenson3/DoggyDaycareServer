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
    public class CreatePetCommandTest
    {
        private readonly Mock<IPetRepository> _repository;

        public CreatePetCommandTest()
        {
            _repository = new Mock<IPetRepository>();
        }

        [Fact]
        public async void ShouldCreatePet()
        {
            // Arrange
            var pet = new Pet {
                Id = "2",
                Name = "Stevie",
                CustomerId = "1"
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
