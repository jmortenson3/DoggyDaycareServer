using DoggyDaycare.Core.Common;
using DoggyDaycare.Core.Users;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xunit;

namespace DoggyDaycare.Core.Tests.Customers
{
    public class GetUserQueryTest
    {
        private readonly Mock<IAsyncRepository<User>> _repository;

        public GetUserQueryTest()
        {
            _repository = new Mock<IAsyncRepository<User>>();
            _repository.Setup(x => x.FindAsync(It.Is<string>(val => val == "1")))
                .ReturnsAsync(new User { Id = "1", FirstName = "Josiah", Email = "test@test.com"} );
        }

        [Fact]
        public async void ShouldReturnUser()
        {
            // Arrange
            var expected = new User
            {
                Id = "1",
                FirstName = "Josiah",
                Email = "test@test.com"
            };
            var query = new GetUserQuery
            {
                Id = expected.Id
            };

            // Act
            var handler = new GetUserQueryHandler(_repository.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expected.Id, result.Id);
            Assert.Equal(expected.FirstName, result.FirstName);
            Assert.Equal(expected.Email, result.Email);

        }

        [Fact]
        public async void ShouldReturnNullUser()
        {
            // Arrange
            var query = new GetUserQuery
            {
                Id = "-1"
            };

            // Act
            var handler = new GetUserQueryHandler(_repository.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Null(result);
        }
    }
}
