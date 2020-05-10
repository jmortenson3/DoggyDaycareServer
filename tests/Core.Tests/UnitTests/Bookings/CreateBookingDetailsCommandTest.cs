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
    public class CreateBookingDetailsCommandTest
    {
        private readonly Mock<IAsyncRepository<BookingDetails>> _repository;

        public CreateBookingDetailsCommandTest()
        {
            var bookingDetails = new BookingDetails
            {
                Id = 2,
                BookingDetailType = BookingDetailType.Boarding
            };

            _repository = new Mock<IAsyncRepository<BookingDetails>>();
            _repository.Setup(x => x.AddAsync(It.IsAny<BookingDetails>()))
                .ReturnsAsync(bookingDetails);
        }

        [Fact]
        public async void ShouldReturnBookingDetails()
        {
            // Arrange
            var bookingDetails = new BookingDetails
            {
                Id = 2,
                BookingDetailType = BookingDetailType.Boarding
            };

            var command = new CreateBookingDetailsCommand
            {
                BookingDetails = bookingDetails
            };

            // Act
            var handler = new CreateBookingDetailsCommandHandler(_repository.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async void ShouldCallAddAsyncOnce()
        {
            // Arrange
            var bookingDetails = new BookingDetails
            {
                Id = 2,
                BookingDetailType = BookingDetailType.Boarding
            };

            var command = new CreateBookingDetailsCommand
            {
                BookingDetails = bookingDetails
            };

            // Act
            var handler = new CreateBookingDetailsCommandHandler(_repository.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            _repository.Verify(x => x.AddAsync(bookingDetails), Times.Once);
        }
    }
}
