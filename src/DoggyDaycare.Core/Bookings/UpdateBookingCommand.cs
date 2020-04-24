using DoggyDaycare.Core.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DoggyDaycare.Core.Bookings.Commands
{
    public class UpdateBookingCommand : IRequest<Booking>
    {
        public Booking Booking { get; set; }
    }

    public class UpdateBookingCommandHandler : IRequestHandler<UpdateBookingCommand, Booking>
    {
        private readonly IAsyncRepository<Booking> _repository;

        public UpdateBookingCommandHandler(IAsyncRepository<Booking> repository)
        {
            _repository = repository;

        }

        public async Task<Booking> Handle(UpdateBookingCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.UpdateAsync(request.Booking);
            return entity;

        }
    }
}
