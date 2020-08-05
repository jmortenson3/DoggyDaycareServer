using Core.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Bookings
{
    public class GetBookingQuery : IRequest<Booking>
    {
        public int Id { get; set; }
    }

    public class GetBookingQueryHandler : IRequestHandler<GetBookingQuery, Booking>
    {
        private readonly IBookingRepository _bookingRepository;

        public GetBookingQueryHandler(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<Booking> Handle(GetBookingQuery request, CancellationToken cancellationToken)
        {
            return await _bookingRepository.FindByIdAsync(request.Id);
        }
    }
}
