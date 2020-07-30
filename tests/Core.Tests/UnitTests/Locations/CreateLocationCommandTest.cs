using Core.Common;
using Core.Locations;
using Core.Memberships;
using Core.Organizations;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xunit;

namespace Core.Tests.UnitTests.Locations
{
    public class CreateLocationCommandTest
    {
        private readonly Mock<ILocationRepository> _locationRepository;
        private readonly Mock<IOrganizationRepository> _organizationRepository;
        private readonly Mock<IMembershipRepository> _membershipRepository;

        public CreateLocationCommandTest()
        {
            _locationRepository = new Mock<ILocationRepository>();
            _organizationRepository = new Mock<IOrganizationRepository>();
            _membershipRepository = new Mock<IMembershipRepository>();

            _organizationRepository.Setup(x => x.Find(It.IsAny<int>(), It.IsAny<string>())).Returns(new Organization { Id = 1 });
        }

        [Fact]
        public async void ShouldReturnLocation()
        {
            // Arrange
            var command = new CreateLocationCommand { OrganizationId = 1, Name = "South Store", CreatedUtc = DateTime.Now };

            // Act
            var handler = new CreateLocationCommandHandler(_locationRepository.Object, _membershipRepository.Object, _organizationRepository.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async void ShouldCallAddOnce()
        {
            // Arrange
            var command = new CreateLocationCommand { OrganizationId = 1, Name = "South Store", CreatedUtc = DateTime.Now };

            // Act
            var handler = new CreateLocationCommandHandler(_locationRepository.Object, _membershipRepository.Object, _organizationRepository.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            _locationRepository.Verify(x => x.Add(It.IsAny<Location>()), Times.Once);
        }
    }
}
