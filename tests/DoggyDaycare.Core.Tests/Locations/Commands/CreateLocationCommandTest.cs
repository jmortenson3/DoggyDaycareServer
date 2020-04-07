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
    public class CreateLocationCommandTest
    {
        private readonly IMediator _mediator;
        private readonly ILocationRepository _repository;

        public CreateLocationCommandTest()
        {
            var services = new ServiceCollection();
            services.AddMediatR(typeof(CreateLocationCommand));
            services.AddScoped<ILocationRepository, MockLocationRepository>();
            var servicesProvider = services.BuildServiceProvider();
            _mediator = servicesProvider.GetService<IMediator>();
            _repository = servicesProvider.GetService<ILocationRepository>();
        }

        [Fact]
        public async void ShouldCreateLocation()
        {
            // Arrange
            var location = new Location
            {
                Id = "1",
                Name = "South Store"
            };
            var command = new CreateLocationCommand
            {
                Location = location
            };

            // Act
            var resultId = await _mediator.Send(command);

            // Assert

            var result = _repository.Find(location.Id);
            Assert.NotNull(result);
            Assert.Equal(location.Id, resultId);
            Assert.Equal(location.Name, result.Name);
        }
    }
}
