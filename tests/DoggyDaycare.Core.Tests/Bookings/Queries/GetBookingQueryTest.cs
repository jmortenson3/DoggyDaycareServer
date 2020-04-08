using DoggyDaycare.Core.Bookings.Entities;
using DoggyDaycare.Core.Bookings.Queries;
using DoggyDaycare.Core.Common;
using DoggyDaycare.Infrastructure;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DoggyDaycare.Core.Tests.Bookings.Queries
{
    public class GetBookingQueryTest
    {
        private readonly IMediator _mediator;

        public GetBookingQueryTest()
        {
            var services = new ServiceCollection();
            services.AddMediatR(typeof(GetBookingQuery));
            services.AddScoped<IBookingRepository, MockBookingRepository>();
            _mediator = services.BuildServiceProvider().GetService<IMediator>();
        }

        [Fact]
        public async void ShouldReturnLocation()
        {
            // Arrange
            var expected = new KennelBooking
            {
                Id = "1"
            };
            var query = new GetBookingQuery
            {
                Id = "1"
            };

            // Act
            var booking = await _mediator.Send(query);

            // Assert
            Assert.Equal(expected.Id, booking.Id);

        }

        [Fact]
        public async void ShouldReturnNullLocation()
        {
            // Arrange
            var query = new GetBookingQuery
            {
                Id = "-1"
            };

            // Act
            var booking = await _mediator.Send(query);

            // Assert
            Assert.Null(booking);
        }
    }
}
