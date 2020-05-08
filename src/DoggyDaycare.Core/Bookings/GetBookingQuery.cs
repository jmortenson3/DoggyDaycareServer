using DoggyDaycare.Core.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DoggyDaycare.Core.Bookings
{
    public class GetBookingQuery : IRequest<Booking>
    {
        public int Id { get; set; }
    }

    public class GetBookingQueryHandler : IRequestHandler<GetBookingQuery, Booking>
    {
        private readonly IAsyncRepository<Booking> _repository;

        public GetBookingQueryHandler(IAsyncRepository<Booking> repository)
        {
            _repository = repository;
        }

        public async Task<Booking> Handle(GetBookingQuery request, CancellationToken cancellationToken)
        {
            return await _repository.FindAsync(request.Id);
        }
    }
}
