using DoggyDaycare.Core.Common;
using DoggyDaycare.Core.Organizations.Entities;
using DoggyDaycare.Core.Organizations.Queries;
using DoggyDaycare.Infrastructure;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DoggyDaycare.Core.Tests.Organizations.Queries
{
    public class GetOrganizationQueryTest
    {
        private readonly IMediator _mediator;

        public GetOrganizationQueryTest()
        {
            var services = new ServiceCollection();
            services.AddMediatR(typeof(GetOrganizationQuery));
            services.AddSingleton<IOrganizationRepository, MockOrganizationRepository>();
            var servicesProvider = services.BuildServiceProvider();
            _mediator = servicesProvider.GetService<IMediator>();
        }

        [Fact]
        public async void ShouldGetOrganization()
        {
            // Arrange
            var expected = new Organization
            {
                Id = "1",
                Name = "DoggyDaycare"
            };

            var query = new GetOrganizationQuery
            {
                Id = expected.Id
            };

            // Act
            var result = await _mediator.Send(query);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expected.Name, result.Name);
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
            var organization = await _mediator.Send(query);

            // Assert
            Assert.Null(organization);
        }
    }
}
