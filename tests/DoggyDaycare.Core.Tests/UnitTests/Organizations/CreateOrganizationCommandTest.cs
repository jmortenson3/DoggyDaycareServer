using DoggyDaycare.Core.Common;
using DoggyDaycare.Core.Organizations;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xunit;

namespace DoggyDaycare.Core.Tests.Organizations
{
    public class CreateOrganizationCommandTest
    {
        private readonly Mock<IAsyncRepository<Organization>> _repository;

        public CreateOrganizationCommandTest()
        {
            _repository = new Mock<IAsyncRepository<Organization>>();
            _repository.Setup(x => x.AddAsync(It.IsAny<Organization>()))
                .ReturnsAsync(new Organization { Id = 2, Name = "St. Larry's" });
        }

        [Fact]
        public async void ShouldCreateOrganization()
        {
            // Arrange
            var organization = new Organization
            {
                Id = 2,
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
