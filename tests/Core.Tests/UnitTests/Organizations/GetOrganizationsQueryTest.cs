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
    public class GetOrganizationsQueryTest
    {
        private readonly Mock<IOrganizationRepository> _repository;

        public GetOrganizationsQueryTest()
        {

            var organizations = new List<Organization>
            {
                new Organization
                {
                    Id = 1,
                    Name = "DoggyDaycare"
                }
            };

            _repository = new Mock<IOrganizationRepository>();
            _repository.Setup(x => x.FindAllAsync(It.IsAny<string>())).ReturnsAsync(organizations);
        }

        [Fact]
        public async void ShouldReturnOrganizations()
        {
            // Arrange
            var query = new GetOrganizationsQuery();

            // Act
            var handler = new GetOrganizationsQueryHandler(_repository.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async void ShouldCallFindOnce()
        {
            // Arrange
            var query = new GetOrganizationsQuery();

            // Act
            var handler = new GetOrganizationsQueryHandler(_repository.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            _repository.Verify(x => x.FindAllAsync(It.IsAny<string>()), Times.Once); ;
        }
    }
}
