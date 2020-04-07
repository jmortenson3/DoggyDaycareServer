using DoggyDaycare.Core.Common;
using DoggyDaycare.Core.Organizations.Commands;
using DoggyDaycare.Core.Organizations.Entities;
using DoggyDaycare.Infrastructure;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DoggyDaycare.Core.Tests.Organizations.Commands
{
    public class UpdateOrganizationCommandTest
    {
        private readonly IMediator _mediator;

        public UpdateOrganizationCommandTest()
        {
            var services = new ServiceCollection();
            services.AddMediatR(typeof(UpdateOrganizationCommand));
            services.AddSingleton<IOrganizationRepository, MockOrganizationRepository>();
            var servicesProvider = services.BuildServiceProvider();
            _mediator = servicesProvider.GetService<IMediator>();
        }

        [Fact]
        public async void ShouldUpdateOrganization()
        {
            // Arrange
            var updatedOrganization = new Organization
            {
                Id = "1",
                Name = "A new name"
            };

            var command = new UpdateOrganizationCommand
            {
                Organization = updatedOrganization
            };

            // Act
            var result = await _mediator.Send(command);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(result.Name, updatedOrganization.Name);

        }
    }
}
