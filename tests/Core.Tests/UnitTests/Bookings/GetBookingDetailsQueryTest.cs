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
    public class GetBookingDetailsQueryTest
    {
        private readonly Mock<IAsyncRepository<BookingDetails>> _repository;

        public GetBookingDetailsQueryTest()
        {
            _repository = new Mock<IAsyncRepository<BookingDetails>>();
            _repository.Setup(x => x.FindAsync(1))
                .ReturnsAsync(new BookingDetails { Id = 1 });
        }

        [Fact]
        public async void ShouldReturnBookingDetails()
        {
            // Arrange
            var query = new GetBookingDetailsQuery
            {
                Id = 1
            };

            // Act
            var handler = new GetBookingDetailsQueryHandler(_repository.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async void ShouldCallFindAsyncOnce()
        {
            // Arrange
            var query = new GetBookingDetailsQuery
            {
                Id = 1
            };

            // Act
            var handler = new GetBookingDetailsQueryHandler(_repository.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            _repository.Verify(x => x.FindAsync(query.Id), Times.Once);
        }
    }
}
