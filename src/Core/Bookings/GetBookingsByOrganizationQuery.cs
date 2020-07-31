using Core.Exceptions;
using Core.Organizations;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Bookings
{
    public class GetBookingsByOrganizationQuery : IRequest<List<Booking>>
    {
        public string UserId { get; set; }
        public int OrganizationId { get; set; }
    }

    public class GetBookingsByOrganizationQueryHandler : IRequestHandler<GetBookingsByOrganizationQuery, List<Booking>>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IOrganizationRepository _organizationRepository;

        public GetBookingsByOrganizationQueryHandler(IBookingRepository bookingRepository, IOrganizationRepository organizationRepository)
        {
            _bookingRepository = bookingRepository;
            _organizationRepository = organizationRepository;
        }

        public async Task<List<Booking>> Handle(GetBookingsByOrganizationQuery request, CancellationToken cancellationToken)
        {
            var organization = _organizationRepository.Find(request.OrganizationId, request.UserId);
            if (organization == null)
            {
                throw new OrganizationNotFoundException($"Organization with id {request.OrganizationId} not found.");
            }
            var bookings = await _bookingRepository.FindByOrganizationAsync(organization.Id);
            return bookings;
        }
    }

}
