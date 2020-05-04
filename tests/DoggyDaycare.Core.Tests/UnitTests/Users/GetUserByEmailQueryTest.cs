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
    public class GetUserByEmailQueryTest
    {
        private readonly Mock<IAsyncRepository<User>> _repository;

        public GetUserByEmailQueryTest()
        {
            _repository = new Mock<IAsyncRepository<User>>();
            _repository.Setup(x => x.FindAllAsync(It.IsAny<Func<User, bool>>()))
                .ReturnsAsync(new List<User> { new User { Id = "1", FirstName = "Josiah", Email = "test@test.com" } } );
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

            var query = new GetUserByEmailQuery
            {
                Email = expected.Email
            };

            // Act
            var handler = new GetUserByEmailQueryHandler(_repository.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expected.Id, result.Id);
            Assert.Equal(expected.FirstName, result.FirstName);
            Assert.Equal(expected.Email, result.Email);

        }
    }
}
