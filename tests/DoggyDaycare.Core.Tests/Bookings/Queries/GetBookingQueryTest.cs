using DoggyDaycare.Core.Bookings.Entities;
using DoggyDaycare.Core.Bookings.Queries;
using DoggyDaycare.Core.Common;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xunit;

namespace DoggyDaycare.Core.Tests.Bookings.Queries
{
    public class GetBookingQueryTest
    {
        private readonly Mock<IBookingRepository> _repository;

        public GetBookingQueryTest()
        {
            _repository = new Mock<IBookingRepository>();
        }

        [Fact]
        public async void ShouldReturnLocation()
        {
            // Arrange
            var expected = new KennelBooking
            {
                Id = "1"
            };
            var query = new GetBookingQuery
            {
                Id = "1"
            };

            // Act
            var handler = new GetBookingQueryHandler(_repository.Object);
            var booking = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(booking);
            Assert.Equal(expected.Id, booking.Id);

        }

        [Fact]
        public async void ShouldReturnNullLocation()
        {
            // Arrange
            var query = new GetBookingQuery
            {
                Id = "-1"
            };

            // Act
            var handler = new GetBookingQueryHandler(_repository.Object);
            var booking = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Null(booking);
        }
    }
}
