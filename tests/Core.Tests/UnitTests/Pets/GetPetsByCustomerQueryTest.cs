﻿using Core.Common;
using Core.Pets;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using Xunit;

namespace Core.Tests.UnitTests.Pets
{
    public class GetPetsByCustomerQueryTest
    {
        private readonly Mock<IPetRepository> _repository;

        public GetPetsByCustomerQueryTest()
        {
            _repository = new Mock<IPetRepository>();
            _repository.Setup(x => x.FindAsync(It.IsAny<Func<Pet, bool>>()))
                .ReturnsAsync(new List<Pet> { new Pet { Id = 1, Name = "Larry", OwnerId = "1" } });
        }

        [Fact]
        public async void ShouldReturnPets()
        {
            // Arrange
            var query = new GetPetsByCustomerQuery { OwnerId = "1" };

            // Act
            var handler = new GetPetsByCustomerQueryHandler(_repository.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async void ShouldCallFindAllAsyncOnce()
        {
            // Arrange
            var query = new GetPetsByCustomerQuery { OwnerId = "1" };

            // Act
            var handler = new GetPetsByCustomerQueryHandler(_repository.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            _repository.Verify(x => x.FindAsync(It.IsAny<Func<Pet, bool>>()), Times.Once);
        }
    }
}
