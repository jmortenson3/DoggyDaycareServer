using DoggyDaycare.Core.Common;
using DoggyDaycare.Core.Pets;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using Xunit;

namespace DoggyDaycare.Core.Tests.Pets
{
    public class GetPetsByCustomerQueryTest
    {
        private readonly Mock<IAsyncRepository<Pet>> _repository;

        public GetPetsByCustomerQueryTest()
        {
            _repository = new Mock<IAsyncRepository<Pet>>();
            // How to do this?
            _repository.Setup(x => x.FindAllAsync(It.IsAny<Predicate<Pet>>()))
                .ReturnsAsync(new List<Pet> { new Pet { Id = "1", Name = "Larry", CustomerId = "1" } });
        }

        [Fact]
        public async void ShouldReturnPetsForDefaultCustomer()
        {
            // Arrange
            var expected = new Pet
            {
                Id = "1",
                Name = "Larry",
                CustomerId = "1"
            };
            var query = new GetPetsByCustomerQuery
            {
                CustomerId = expected.CustomerId
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
            Assert.Equal(expected.CustomerId, resultPet.CustomerId);
        }
    }
}
