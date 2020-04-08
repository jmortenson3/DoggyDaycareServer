using DoggyDaycare.Core.Bookings.Commands;
using DoggyDaycare.Core.Bookings.Entities;
using DoggyDaycare.Core.Common;
using DoggyDaycare.Infrastructure;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DoggyDaycare.Core.Tests.Bookings.Commands
{
    public class CreateBookingCommandTest
    {
        private readonly IMediator _mediator;
        private readonly IBookingRepository _repository;

        public CreateBookingCommandTest()
        {
            var services = new ServiceCollection();
            services.AddMediatR(typeof(CreateBookingCommand));
            services.AddSingleton<IBookingRepository, MockBookingRepository>();
            var serviceProvider = services.BuildServiceProvider();
            _mediator = serviceProvider.GetService<IMediator>();
            _repository = serviceProvider.GetService<IBookingRepository>();
        }

        [Fact]
        public async void ShouldCreateGroomingBooking()
        {
            // Arrange
            var booking = new GroomingBooking
            {
                Id = "2"
            };

            var command = new CreateBookingCommand
            {
                Booking = booking
            };

            // Act
            await _mediator.Send(command);
            var result = _repository.Find(command.Booking.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(command.Booking.Id, result.Id);
        }
    }
}
