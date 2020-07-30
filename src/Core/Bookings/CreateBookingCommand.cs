using Core.Common;
using Core.Locations;
using Core.Organizations;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Bookings
{
    public class CreateBookingCommand : IRequest<Booking>
    {
        public int LocationId { get; set; }
        public int OrganizationId { get; set; }
        public string OwnerId { get; set; }
        [JsonIgnore]
        public string CreatedBy { get; set; }
        [JsonIgnore]
        public DateTime CreatedUtc { get; set; }
        public List<BookingDetails> BookingDetails { get; set; }
    }

    public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, Booking>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IOrganizationRepository _organizationRepository;
        private readonly ILocationRepository _locationRepository;

        public CreateBookingCommandHandler(
            IBookingRepository bookingRepository, 
            IOrganizationRepository organizationRepository,
            ILocationRepository locationRepository)
        {
            _bookingRepository = bookingRepository;
            _organizationRepository = organizationRepository;
            _locationRepository = locationRepository;
        }

        public async Task<Booking> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            var organization = await _organizationRepository.FindAsync(request.OrganizationId);
            if (organization == null)
            {
                return null;
            }

            var location = await _locationRepository.FindAsync(request.LocationId);

            if (location == null)
            {
                return null;
            }

            var bookingDetails = new List<BookingDetails>();

            foreach (var bookingDetail in request.BookingDetails)
            {
                bookingDetails.Add(bookingDetail);
            }

            var booking = new Booking
            {
                OwnerId = request.OwnerId,
                OrganizationId = request.OrganizationId,
                LocationId = request.LocationId,
                CreatedBy = request.CreatedBy,
                CreatedUtc = request.CreatedUtc,
                BookingDetails = bookingDetails,
                Organization = organization,
                Location = location
            };
            _bookingRepository.Add(booking);
            await _bookingRepository.SaveAsync();

            return booking;
        }
    }
}
