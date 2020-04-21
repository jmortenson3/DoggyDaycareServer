using DoggyDaycare.Core.Common;
using DoggyDaycare.Core.Organizations.Entities;
using DoggyDaycare.Core.Organizations.Queries;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xunit;

namespace DoggyDaycare.Core.Tests.Organizations.Queries
{
    public class GetOrganizationQueryTest
    {
        private readonly Mock<IOrganizationRepository> _repository;

        public GetOrganizationQueryTest()
        {

            var organization = new Organization
            {
                Id = "1",
                Name = "DoggyDaycare"
            };

            _repository = new Mock<IOrganizationRepository>();
            _repository.Setup(x => x.FindAsync(It.IsAny<string>())).ReturnsAsync(organization);
        }

        [Fact]
        public async void ShouldGetOrganization()
        {
            // Arrange

            var query = new GetOrganizationQuery
            {
                Id = "1"
            };

            // Act
            var handler = new GetOrganizationQueryHandler(_repository.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async void ShouldReturnNullOrganization()
        {
            // Arrange
            var query = new GetOrganizationQuery
            {
                Id = "-1"
            };

            // Act
            var handler = new GetOrganizationQueryHandler(_repository.Object);
            var organization = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Null(organization);
        }
    }
}
