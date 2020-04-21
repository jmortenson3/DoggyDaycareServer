﻿using DoggyDaycare.Core.Bookings.Commands;
using DoggyDaycare.Core.Bookings.Entities;
using DoggyDaycare.Core.Common;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xunit;

namespace DoggyDaycare.Core.Tests.Bookings.Commands
{
    public class CreateBookingCommandTest
    {
        private readonly Mock<IBookingRepository> _repository;

        public CreateBookingCommandTest()
        {
            var booking = new GroomingBooking
            {
                Id = "2"
            };

            _repository = new Mock<IBookingRepository>();
            _repository.Setup(x => x.AddAsync(It.IsAny<Booking>())).ReturnsAsync(booking);
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
            var handler = new CreateBookingCommandHandler(_repository.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
        }
    }
}
