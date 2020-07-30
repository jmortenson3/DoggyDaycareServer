using Core.Bookings;
using Core.Common;
using Core.Locations;
using Core.Organizations;
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
        private readonly Mock<IBookingRepository> _bookingRepository;
        private readonly Mock<IOrganizationRepository> _organizationRepository;
        private readonly Mock<ILocationRepository> _locationRepository;

        public CreateBookingCommandTest()
        {
            var booking = new Booking
            {
                Id = 2
            };

            _bookingRepository = new Mock<IBookingRepository>();
            _organizationRepository = new Mock<IOrganizationRepository>();
            _locationRepository = new Mock<ILocationRepository>();

            _organizationRepository.Setup(x => x.FindAsync(It.IsAny<int>())).ReturnsAsync(new Organization { Id = 1 });
            _locationRepository.Setup(x => x.FindAsync(It.IsAny<int>())).ReturnsAsync(new Location { Id = 1 });
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
            var handler = new CreateBookingCommandHandler(_bookingRepository.Object, _organizationRepository.Object, _locationRepository.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async void ShouldCallAddOnce()
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
            var handler = new CreateBookingCommandHandler(_bookingRepository.Object, _organizationRepository.Object, _locationRepository.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            _bookingRepository.Verify(x => x.Add(It.IsAny<Booking>()), Times.Once);
        }
    }
}
