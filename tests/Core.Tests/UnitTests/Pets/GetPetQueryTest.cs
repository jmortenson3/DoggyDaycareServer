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
    public class GetPetQueryTest
    {
        private readonly Mock<IPetRepository> _repository;

        public GetPetQueryTest()
        {
            _repository = new Mock<IPetRepository>();
            _repository.Setup(x => x.FindById(It.Is<int>(val => val == 1)))
                .ReturnsAsync(new Pet { Id = 1, Name = "Larry", OwnerId = "1" });
        }

        [Fact]
        public async void ShouldReturnPet()
        {
            // Arrange
            var query = new GetPetQuery(1);

            // Act
            var handler = new GetPetQueryHandler(_repository.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async void ShouldCallFindAsyncOnce()
        {
            // Arrange
            var query = new GetPetQuery(1);

            // Act
            var handler = new GetPetQueryHandler(_repository.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            _repository.Verify(x => x.FindById(1), Times.Once);
        }
    }
}
