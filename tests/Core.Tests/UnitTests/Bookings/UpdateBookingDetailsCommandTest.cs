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
    public class UpdateBookingDetailsCommandTest
    {
        private readonly Mock<IAsyncRepository<BookingDetails>> _repository;

        public UpdateBookingDetailsCommandTest()
        {
            _repository = new Mock<IAsyncRepository<BookingDetails>>();
            _repository.Setup(x => x.UpdateAsync(It.IsAny<BookingDetails>()))
                .ReturnsAsync(new BookingDetails { Id = 1 });
        }

        [Fact]
        public async void ShouldReturnBookingDetails()
        {
            // Arrange
            var bookingDetails = new BookingDetails { Id = 1 };
            var command = new UpdateBookingDetailsCommand { BookingDetails = bookingDetails };
            var handler = new UpdateBookingDetailsCommandHandler(_repository.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async void ShouldCallUpdateAsyncOnce()
        {
            // Arrange
            var bookingDetails = new BookingDetails { Id = 1 };
            var command = new UpdateBookingDetailsCommand { BookingDetails = bookingDetails };
            var handler = new UpdateBookingDetailsCommandHandler(_repository.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            _repository.Verify(x => x.UpdateAsync(bookingDetails), Times.Once);
            Assert.NotNull(result);
        }
    }
}
