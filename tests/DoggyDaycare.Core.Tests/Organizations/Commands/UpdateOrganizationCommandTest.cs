using DoggyDaycare.Core.Common;
using DoggyDaycare.Core.Organizations.Commands;
using DoggyDaycare.Core.Organizations.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xunit;

namespace DoggyDaycare.Core.Tests.Organizations.Commands
{
    public class UpdateOrganizationCommandTest
    {
        private readonly Mock<IOrganizationRepository> _repository;

        public UpdateOrganizationCommandTest()
        {
            _repository = new Mock<IOrganizationRepository>();
        }

        [Fact]
        public async void ShouldUpdateOrganization()
        {
            // Arrange
            var updatedOrganization = new Organization
            {
                Id = "1",
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
