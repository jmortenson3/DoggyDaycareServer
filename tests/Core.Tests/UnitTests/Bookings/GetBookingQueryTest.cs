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
    public class GetBookingQueryTest
    {
        private readonly Mock<IBookingRepository> _repository;

        public GetBookingQueryTest()
        {
            _repository = new Mock<IBookingRepository>();
            _repository.Setup(x => x.FindById(It.Is<int>(val => val == 1)))
                .ReturnsAsync(new Booking { Id = 1 });
        }

        [Fact]
        public async void ShouldReturnBooking()
        {
            // Arrange
            var query = new GetBookingQuery
            {
                Id = 1
            };

            // Act
            var handler = new GetBookingQueryHandler(_repository.Object);
            var booking = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(booking);

        }

        [Fact]
        public async void ShouldReturnNullBooking()
        {
            // Arrange
            var query = new GetBookingQuery
            {
                Id = -1
            };

            // Act
            var handler = new GetBookingQueryHandler(_repository.Object);
            var booking = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Null(booking);
        }
    }
}
