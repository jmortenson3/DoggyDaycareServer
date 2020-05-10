using Core.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Bookings
{
    public class CreateBookingDetailsCommand : IRequest<BookingDetails>
    {
        public BookingDetails BookingDetails { get; set; }
    }

    public class CreateBookingDetailsCommandHandler : IRequestHandler<CreateBookingDetailsCommand, BookingDetails>
    {
        private readonly IAsyncRepository<BookingDetails> _repository;
        public CreateBookingDetailsCommandHandler(IAsyncRepository<BookingDetails> repository)
        {
            _repository = repository;
        }

        public async Task<BookingDetails> Handle(CreateBookingDetailsCommand request, CancellationToken cancellationToken)
        {
            return await _repository.AddAsync(request.BookingDetails);
        }
    }
}
