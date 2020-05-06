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
    public class GetPetQueryTest
    {
        private readonly Mock<IAsyncRepository<Pet>> _repository;

        public GetPetQueryTest()
        {
            _repository = new Mock<IAsyncRepository<Pet>>();
            _repository.Setup(x => x.FindAsync(It.Is<string>(val => val == "1")))
                .ReturnsAsync(new Pet { Id = "1", Name = "Larry", OwnerId = "1" });
        }

        [Fact]
        public async void ShouldReturnPet()
        {
            // Arrange
            var expected = new Pet
            {
                Id = "1",
                Name = "Larry",
                OwnerId = "1"
            };
            var query = new GetPetQuery
            {
                Id = expected.Id
            };

            // Act
            var handler = new GetPetQueryHandler(_repository.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Equal(expected.Id, result.Id);
            Assert.Equal(expected.Name, result.Name);
            Assert.Equal(expected.OwnerId, result.OwnerId);
        }
    }
}
