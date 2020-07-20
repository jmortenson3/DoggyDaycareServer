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
    public class GetOrganizationQueryTest
    {
        private readonly Mock<IOrganizationRepository> _repository;

        public GetOrganizationQueryTest()
        {

            var organization = new Organization
            {
                Id = 1,
                Name = "DoggyDaycare"
            };

            _repository = new Mock<IOrganizationRepository>();
            _repository.Setup(x => x.FindById(It.IsAny<int>())).ReturnsAsync(organization);
        }

        [Fact]
        public async void ShouldReturnOrganization()
        {
            // Arrange
            var query = new GetOrganizationQuery { Id = 1 };

            // Act
            var handler = new GetOrganizationQueryHandler(_repository.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async void ShouldCallFindAsyncOnce()
        {
            // Arrange
            var query = new GetOrganizationQuery { Id = 1 };

            // Act
            var handler = new GetOrganizationQueryHandler(_repository.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            _repository.Verify(x => x.FindById(1), Times.Once);
        }
    }
}
