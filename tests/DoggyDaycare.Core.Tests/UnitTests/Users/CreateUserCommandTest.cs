using System;
using Xunit;
using DoggyDaycare.Core.Common;
using Moq;
using System.Threading;
using DoggyDaycare.Core.Users;

namespace DoggyDaycare.Core.Tests.UnitTests.Users
{
    public class CreateUserCommandTest
    {
        private readonly Mock<IAsyncRepository<User>> _repository;

        public CreateUserCommandTest()
        {
            _repository = new Mock<IAsyncRepository<User>>();
            _repository.Setup(x => x.AddAsync(It.IsAny<User>()))
                .ReturnsAsync(new User { Id = "2", Email = "test2@test.com", FirstName = "Josiah2" });
        }

        [Fact]
        public async void ShouldCreateUser()
        {
            // Arrange
            var user = new User
            {
                Id = "2",
                Email = "test2@test.com",
                FirstName = "Josiah2"
            };

            var command = new CreateUserCommand
            {
                User = user
            };

            // Act
            var handler = new CreateUserCommandHandler(_repository.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);

        }
    }
}
