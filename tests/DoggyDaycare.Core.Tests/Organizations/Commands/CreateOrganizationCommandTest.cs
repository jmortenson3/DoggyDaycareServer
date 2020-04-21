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
    public class CreateOrganizationCommandTest
    {
        private readonly Mock<IOrganizationRepository> _repository;

        public CreateOrganizationCommandTest()
        {
            _repository = new Mock<IOrganizationRepository>();
        }

        [Fact]
        public async void ShouldCreateOrganization()
        {
            // Arrange
            var organization = new Organization
            {
                Id = "2",
                Name = "St. Larry's"
            };
            var command = new CreateOrganizationCommand
            {
                Organization = organization
            };

            // Act
            var handler = new CreateOrganizationCommandHandler(_repository.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
        }
    }
}
