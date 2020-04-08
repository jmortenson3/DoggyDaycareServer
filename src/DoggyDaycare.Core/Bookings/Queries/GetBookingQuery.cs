using DoggyDaycare.Core.Bookings.Entities;
using DoggyDaycare.Core.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DoggyDaycare.Core.Bookings.Queries
{
    public class GetBookingQuery : IRequest<Booking>
    {
        public string Id { get; set; }
    }

    public class GetBookingQueryHandler : IRequestHandler<GetBookingQuery, Booking>
    {
        private readonly IBookingRepository _repository;

        public GetBookingQueryHandler(IBookingRepository repository)
        {
            _repository = repository;
        }

        public async Task<Booking> Handle(GetBookingQuery request, CancellationToken cancellationToken)
        {
            return _repository.Find(request.Id);
        }
    }
}
