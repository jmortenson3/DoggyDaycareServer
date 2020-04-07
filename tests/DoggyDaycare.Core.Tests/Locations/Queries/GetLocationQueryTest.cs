using DoggyDaycare.Core.Common;
using DoggyDaycare.Core.Locations.Entities;
using DoggyDaycare.Core.Locations.Queries;
using DoggyDaycare.Infrastructure;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DoggyDaycare.Core.Tests.Locations.Queries
{
    public class GetLocationQueryTest
    {
        private readonly IMediator _mediator;

        public GetLocationQueryTest()
        {
            var services = new ServiceCollection();
            services.AddMediatR(typeof(GetLocationQuery));
            services.AddScoped<ILocationRepository, MockLocationRepository>();
            _mediator = services.BuildServiceProvider().GetService<IMediator>();
        }

        [Fact]
        public async void ShouldReturnLocation()
        {
            // Arrange
            var expected = new Location
            {
                Id = "1",
                Name = "South Store"
            };
            var query = new GetLocationQuery
            {
                Id = "1"
            };

            // Act
            var location = await _mediator.Send(query);

            // Assert
            Assert.Equal(expected.Id, location.Id);
            Assert.Equal(expected.Name, location.Name);

        }

        [Fact]
        public async void ShouldReturnNullLocation()
        {
            // Arrange
            var query = new GetLocationQuery
            {
                Id = "-1"
            };

            // Act
            var location = await _mediator.Send(query);

            // Assert
            Assert.Null(location);
        }
    }
}
