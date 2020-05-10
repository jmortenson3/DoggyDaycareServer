using Core.Bookings;
using Core.Common;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xunit;

namespace Core.Tests.Bookings
{
    public class UpdateBookingCommandTest
    {
        private readonly Mock<IAsyncRepository<Booking>> _repository;

        public UpdateBookingCommandTest()
        {
            _repository = new Mock<IAsyncRepository<Booking>>();
            _repository.Setup(x => x.UpdateAsync(It.IsAny<Booking>()))
                .ReturnsAsync(new KennelBooking { Id = 1 });
        }

        [Fact]
        public async void ShouldUpdateBooking()
        {
            // Arrange
            var booking = new KennelBooking
            {
                Id = 1
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
