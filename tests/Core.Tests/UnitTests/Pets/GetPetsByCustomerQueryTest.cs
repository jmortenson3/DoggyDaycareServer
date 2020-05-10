using Core.Common;
using Core.Pets;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using Xunit;

namespace Core.Tests.Pets
{
    public class GetPetsByCustomerQueryTest
    {
        private readonly Mock<IAsyncRepository<Pet>> _repository;

        public GetPetsByCustomerQueryTest()
        {
            _repository = new Mock<IAsyncRepository<Pet>>();
            // How to do this?
            _repository.Setup(x => x.FindAllAsync(It.IsAny<Func<Pet, bool>>()))
                .ReturnsAsync(new List<Pet> { new Pet { Id = 1, Name = "Larry", OwnerId = "1" } });
        }

        [Fact]
        public async void ShouldReturnPetsForDefaultCustomer()
        {
            // Arrange
            var expected = new Pet
            {
                Id = 1,
                Name = "Larry",
                OwnerId = "1"
            };
            var query = new GetPetsByCustomerQuery
            {
                OwnerId = expected.OwnerId
            };

            // Act
            var handler = new GetPetsByCustomerQueryHandler(_repository.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.Count > 0);
            var resultPet = result.Find(pet => pet.Id == expected.Id);
            Assert.NotNull(resultPet);
            Assert.Equal(expected.Id, resultPet.Id);
            Assert.Equal(expected.Name, resultPet.Name);
            Assert.Equal(expected.OwnerId, resultPet.OwnerId);
        }
    }
}
