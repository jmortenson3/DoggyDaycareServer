using Core.Exceptions;
using Core.Locations;
using Core.Organizations;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Bookings
{
    public class GetBookingsByLocationQuery : IRequest<List<Booking>>
    {
        public string UserId { get; set; }
        public int LocationId { get; set; }
    }

    public class GetBookingsByLocationQueryHandler : IRequestHandler<GetBookingsByLocationQuery, List<Booking>>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly ILocationRepository _locationRepository;

        public GetBookingsByLocationQueryHandler(IBookingRepository bookingRepository, ILocationRepository locationRepository)
        {
            _bookingRepository = bookingRepository;
            _locationRepository = locationRepository;
        }

        public async Task<List<Booking>> Handle(GetBookingsByLocationQuery request, CancellationToken cancellationToken)
        {
            var location = await _locationRepository.FindAsync(request.LocationId);
            if (location == null)
            {
                throw new LocationNotFoundException($"Location with id {request.LocationId} not found");
            }
            var bookings = await _bookingRepository.FindByLocationAsync(location.Id);
            return bookings;
        }
    }

}
