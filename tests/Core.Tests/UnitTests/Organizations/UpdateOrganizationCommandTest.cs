using Core.Common;
using Core.Organizations;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xunit;

namespace Core.Tests.UnitTests.Organizations
{
    public class UpdateOrganizationCommandTest
    {
        private readonly Mock<IAsyncRepository<Organization>> _repository;

        public UpdateOrganizationCommandTest()
        {
            _repository = new Mock<IAsyncRepository<Organization>>();
            _repository.Setup(x => x.UpdateAsync(It.IsAny<Organization>()))
                .ReturnsAsync(new Organization { Id = 1, Name = "A new name" });
        }

        [Fact]
        public async void ShouldUpdateOrganization()
        {
            // Arrange
            var updatedOrganization = new Organization
            {
                Id = 1,
                Name = "A new name"
            };

            var command = new UpdateOrganizationCommand
            {
                Organization = updatedOrganization
            };

            // Act
            var handler = new UpdateOrganizationCommandHandler(_repository.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(result.Name, updatedOrganization.Name);

        }
    }
}
