using DoggyDaycare.Core.Common;
using DoggyDaycare.Core.Locations.Commands;
using DoggyDaycare.Core.Locations.Entities;
using DoggyDaycare.Infrastructure;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DoggyDaycare.Core.Tests.Locations.Commands
{
    public class UpdateLocationCommandTest
    {
        private readonly IMediator _mediator;
        private readonly ILocationRepository _repository;

        public UpdateLocationCommandTest()
        {
            var services = new ServiceCollection();
            services.AddMediatR(typeof(UpdateLocationCommand));
            services.AddScoped<ILocationRepository, MockLocationRepository>();
            var servicesProvider = services.BuildServiceProvider();
            _mediator = servicesProvider.GetService<IMediator>();
            _repository = servicesProvider.GetService<ILocationRepository>();
        }

        [Fact]
        public async void ShouldUpdateLocation()
        {
            // Arrange
            var updatedLocation = new Location
            {
                Id = "1",
                Name = "A new name"
            };

            var command = new UpdateLocationCommand
            {
                Location = updatedLocation
            };

            // Act
            var result = await _mediator.Send(command);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(result.Name, updatedLocation.Name);
        }
    }
}
