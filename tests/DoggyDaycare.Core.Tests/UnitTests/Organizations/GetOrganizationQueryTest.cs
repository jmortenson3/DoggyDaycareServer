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
    public class GetOrganizationQueryTest
    {
        private readonly Mock<IAsyncRepository<Organization>> _repository;

        public GetOrganizationQueryTest()
        {

            var organization = new Organization
            {
                Id = 1,
                Name = "DoggyDaycare"
            };

            _repository = new Mock<IAsyncRepository<Organization>>();
            _repository.Setup(x => x.FindAsync(It.IsAny<int>())).ReturnsAsync(organization);
        }

        [Fact]
        public async void ShouldGetOrganization()
        {
            // Arrange
            var query = new GetOrganizationQuery
            {
                Id = 1
            };

            // Act
            var handler = new GetOrganizationQueryHandler(_repository.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
        }
    }
}
