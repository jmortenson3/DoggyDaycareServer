using Core.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Bookings
{
    public class GetBookingDetailsQuery : IRequest<BookingDetails>
    {
        public int Id { get; set; }
    }

    public class GetBookingDetailsQueryHandler : IRequestHandler<GetBookingDetailsQuery, BookingDetails>
    {
        private readonly IAsyncRepository<BookingDetails> _repository;

        public GetBookingDetailsQueryHandler(IAsyncRepository<BookingDetails> repository)
        {
            _repository = repository;
        }

        public async Task<BookingDetails> Handle(GetBookingDetailsQuery request, CancellationToken cancellationToken)
        {
            return await _repository.FindAsync(request.Id);
        }
    }
}
