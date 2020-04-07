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
    public class CreateOrganizationCommandTest
    {
        private readonly IMediator _mediator;
        private readonly IOrganizationRepository _repository;

        public CreateOrganizationCommandTest()
        {
            var services = new ServiceCollection();
            services.AddMediatR(typeof(CreateOrganizationCommand));
            services.AddSingleton<IOrganizationRepository, MockOrganizationRepository>();
            var servicesProvider = services.BuildServiceProvider();
            _mediator = servicesProvider.GetService<IMediator>();
            _repository = servicesProvider.GetService<IOrganizationRepository>();
        }

        [Fact]
        public async void ShouldCreateOrganization()
        {
            // Arrange
            var organization = new Organization
            {
                Id = "2",
                Name = "St. Larry's"
            };
            var command = new CreateOrganizationCommand
            {
                Organization = organization
            };

            // Act
            var resultId = await _mediator.Send(command);

            // Assert
            var result = _repository.Find(organization.Id);
            Assert.NotNull(result);
            Assert.Equal(organization.Id, resultId);
            Assert.Equal(organization.Name, result.Name);
        }
    }
}
