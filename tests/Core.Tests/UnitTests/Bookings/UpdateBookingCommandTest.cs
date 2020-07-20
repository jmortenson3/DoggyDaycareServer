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
    public class UpdateBookingCommandTest
    {
        private readonly Mock<IBookingRepository> _repository;

        public UpdateBookingCommandTest()
        {
            _repository = new Mock<IBookingRepository>();
            _repository.Setup(x => x.Update(It.IsAny<Booking>()))
                .ReturnsAsync(new Booking { Id = 1 });
        }

        [Fact]
        public async void ShouldReturnBooking()
        {
            // Arrange
            var booking = new Booking { Id = 1 };
            var command = new UpdateBookingCommand { Booking = booking };
            var handler = new UpdateBookingCommandHandler(_repository.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async void ShouldCallUpdateAsyncOnce()
        {
            // Arrange
            var booking = new Booking { Id = 1 };
            var command = new UpdateBookingCommand { Booking = booking };
            var handler = new UpdateBookingCommandHandler(_repository.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            _repository.Verify(x => x.Update(booking), Times.Once);
        }
    }
}
