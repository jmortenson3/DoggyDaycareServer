using DoggyDaycare.Core.Bookings.Commands;
using DoggyDaycare.Core.Bookings.Entities;
using DoggyDaycare.Core.Common;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xunit;

namespace DoggyDaycare.Core.Tests.Bookings
{
    public class UpdateBookingCommandTest
    {
        private readonly Mock<IBookingRepository> _repository;

        public UpdateBookingCommandTest()
        {
            _repository = new Mock<IBookingRepository>();
            _repository.Setup(x => x.UpdateAsync(It.IsAny<Booking>())).ReturnsAsync(new KennelBooking { Id = "1" });
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
            var handler = new UpdateBookingCommandHandler(_repository.Object);
            var result = await handler.Handle(command, CancellationToken.None);


            // Assert
            Assert.NotNull(result);
        }
    }
}
