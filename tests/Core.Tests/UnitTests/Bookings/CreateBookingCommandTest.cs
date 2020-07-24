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
        private readonly Mock<IBookingRepository> _repository;

        public CreateBookingCommandTest()
        {
            var booking = new Booking
            {
                Id = 2
            };

            _repository = new Mock<IBookingRepository>();
            _repository.Setup(x => x.Add(It.IsAny<Booking>())).ReturnsAsync(booking);
        }

        [Fact]
        public async void ShouldReturnBooking()
        {
            // Arrange
            var command = new CreateBookingCommand
            {
                OwnerId = "1",
                OrganizationId = 1,
                LocationId = 1,
                CreatedBy = "1",
                CreatedUtc = DateTime.UtcNow,
                BookingDetails = new List<BookingDetails>
                {
                    new BookingDetails
                    {
                        Id = 1,
                    }
                }
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
            var command = new CreateBookingCommand
            {
                OwnerId = "1",
                OrganizationId = 1,
                LocationId = 1,
                CreatedBy = "1",
                CreatedUtc = DateTime.UtcNow,
                BookingDetails = new List<BookingDetails>
                {
                    new BookingDetails
                    {
                        Id = 1,
                    }
                }
            };
            // Act
            var handler = new CreateBookingCommandHandler(_repository.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            _repository.Verify(x => x.Add(It.IsAny<Booking>()), Times.Once);
        }
    }
}
