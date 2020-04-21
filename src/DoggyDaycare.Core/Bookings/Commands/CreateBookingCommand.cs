using DoggyDaycare.Core.Bookings.Entities;
using DoggyDaycare.Core.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DoggyDaycare.Core.Bookings.Commands
{
    public class CreateBookingCommand : IRequest<Booking>
    {
        public Booking Booking { get; set; }
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
            return await _repository.AddAsync(request.Booking);
        }
    }
}
