using Core.Common;
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
        private readonly IBookingRepository _repository;

        public CreateBookingCommandHandler(IBookingRepository repository)
        {
            _repository = repository;
        }

        public async Task<Booking> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
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
                BookingDetails = bookingDetails
            };
            return await _repository.Add(booking);
        }
    }
}
