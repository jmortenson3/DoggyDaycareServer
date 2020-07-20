﻿using Core.Common;
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
            _repository.Setup(x => x.Add(It.IsAny<Pet>()))
                .ReturnsAsync(new Pet { Id = 2, Name = "Stevie", OwnerId = "1" });
        }

        [Fact]
        public async void ShouldReturnPet()
        {
            // Arrange
            var command = new CreatePetCommand("123", "Stevie", DateTime.Now);

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
            var pet = new Pet
            {
                Id = 2,
                Name = "Stevie",
                OwnerId = "1"
            };
            var command = new CreatePetCommand("123", "Stevie", DateTime.Now);

            // Act
            var handler = new CreatePetCommandHandler(_repository.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            _repository.Verify(x => x.Add(It.IsAny<Pet>()), Times.Once);
        }
    }
}
