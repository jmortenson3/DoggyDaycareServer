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
    public class GetOrganizationByIdQueryTest
    {
        private readonly Mock<IOrganizationRepository> _repository;

        public GetOrganizationByIdQueryTest()
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
            var query = new GetOrganizationByIdQuery { Id = 1 };

            // Act
            var handler = new GetOrganizationByIdQueryHandler(_repository.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async void ShouldCallFindByIdOnce()
        {
            // Arrange
            var query = new GetOrganizationByIdQuery { Id = 1 };

            // Act
            var handler = new GetOrganizationByIdQueryHandler(_repository.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            _repository.Verify(x => x.FindById(1), Times.Once);
        }
    }
}
