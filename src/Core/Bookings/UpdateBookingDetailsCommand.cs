using Core.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Bookings
{
    public class UpdateBookingDetailsCommand : IRequest<BookingDetails>
    {
        public BookingDetails BookingDetails { get; set; }
    }

    public class UpdateBookingDetailsCommandHandler : IRequestHandler<UpdateBookingDetailsCommand, BookingDetails>
    {
        private readonly IAsyncRepository<BookingDetails> _repository;

        public UpdateBookingDetailsCommandHandler(IAsyncRepository<BookingDetails> repository)
        {
            _repository = repository;
        }

        public async Task<BookingDetails> Handle(UpdateBookingDetailsCommand request, CancellationToken cancellationToken)
        {
            return await _repository.UpdateAsync(request.BookingDetails);
        }
    }
}
