using Core.Bookings;
using Core.Common;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xunit;

namespace Core.Tests.UnitTests.Bookings
{
    public class CreateBookingCommandTest
    {
        private readonly Mock<IAsyncRepository<Booking>> _repository;

        public CreateBookingCommandTest()
        {
            var booking = new Booking
            {
                Id = 2
            };

            _repository = new Mock<IAsyncRepository<Booking>>();
            _repository.Setup(x => x.AddAsync(It.IsAny<Booking>())).ReturnsAsync(booking);
        }

        [Fact]
        public async void ShouldReturnBooking()
        {
            // Arrange
            var booking = new Booking
            {
                Id = 2
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

        [Fact]
        public async void ShouldCallAddAsyncOnce()
        {
            // Arrange
            var booking = new Booking
            {
                Id = 2
            };

            var command = new CreateBookingCommand
            {
                Booking = booking
            };

            // Act
            var handler = new CreateBookingCommandHandler(_repository.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            _repository.Verify(x => x.AddAsync(booking), Times.Once);
        }
    }
}
