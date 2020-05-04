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
    public class UpdateUserCommandTest
    {
        private readonly Mock<IAsyncRepository<User>> _repository;

        public UpdateUserCommandTest()
        {
            _repository = new Mock<IAsyncRepository<User>>();
            _repository.Setup(x => x.UpdateAsync(It.IsAny<User>())).ReturnsAsync(new User { Id = "1", Email = "newtest@test.com" } );

        }

        [Fact]
        public async void ShouldUpdateUserEmail()
        {
            // Arrange
            var user = new User
            {
                Id = "1",
                Email = "newtest@test.com"
            };

            var command = new UpdateUserCommand
            {
                User = user
            };

            // Act
            var handler = new UpdateUserCommandHandler(_repository.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
        }
    }

}
