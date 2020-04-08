using DoggyDaycare.Core.Bookings.Commands;
using DoggyDaycare.Core.Bookings.Entities;
using DoggyDaycare.Core.Common;
using DoggyDaycare.Core.Customers.Commands;
using DoggyDaycare.Infrastructure;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DoggyDaycare.Core.Tests.Bookings.Commands
{
    public class UpdateBookingCommandTest
    {
        private readonly IMediator _mediator;
        private readonly IBookingRepository _repository;

        public UpdateBookingCommandTest()
        {
            var services = new ServiceCollection();
            services.AddMediatR(typeof(UpdateBookingCommand));
            services.AddScoped<IBookingRepository, MockBookingRepository>();
            var servicesProvider = services.BuildServiceProvider();
            _mediator = servicesProvider.GetService<IMediator>();
            _repository = servicesProvider.GetService<IBookingRepository>();
        }

        [Fact]
        public async void ShouldUpdateBooking()
        {
            // Arrange
            var booking = new KennelBooking
            {
                Id = "1"
            };

            var command = new UpdateBookingCommand
            {
                Booking = booking
            };

            // Act
            await _mediator.Send(command);

            // Assert
            var updatedCustomer = _repository.Find(booking.Id);

            Assert.Equal(booking.Id, updatedCustomer.Id);
        }
    }
}
