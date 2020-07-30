using Core.Common;
using Core.Memberships;
using Core.Organizations;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xunit;

namespace Core.Tests.UnitTests.Organizations
{
    public class CreateOrganizationCommandTest
    {
        private readonly Mock<IOrganizationRepository> _organizationRepository;
        private readonly Mock<IMembershipRepository> _membershipRepository;

        public CreateOrganizationCommandTest()
        {
            _organizationRepository = new Mock<IOrganizationRepository>();
            _membershipRepository = new Mock<IMembershipRepository>();
        }

        [Fact]
        public async void ShouldAddAndSaveWithMembership()
        {
            // Arrange
            var command = new CreateOrganizationCommand
            {
                OwnerId = "1",
                Name = "St. Larry's",
                CreatedUtc = DateTime.UtcNow
            };

            // Act
            var handler = new CreateOrganizationCommandHandler(_organizationRepository.Object, _membershipRepository.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            _organizationRepository.Verify(x => x.Add(It.IsAny<Organization>()), Times.Once);
            _membershipRepository.Verify(x => x.Add(It.IsAny<Membership>()), Times.Once);
            _organizationRepository.Verify(x => x.SaveAsync(), Times.Once);
        }
    }
}
