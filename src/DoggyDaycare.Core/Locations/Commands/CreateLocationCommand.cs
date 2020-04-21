using DoggyDaycare.Core.Common;
using DoggyDaycare.Core.Locations.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DoggyDaycare.Core.Locations.Commands
{
    public class CreateLocationCommand : IRequest<Location>
    {
        public Location Location { get; set; }
    }

    public class CreateLocationCommandHandler : IRequestHandler<CreateLocationCommand, Location>
    {
        private readonly ILocationRepository _repository;

        public CreateLocationCommandHandler(ILocationRepository repository)
        {
            _repository = repository;
        }

        public async Task<Location> Handle(CreateLocationCommand request, CancellationToken cancellationToken)
        {
            return await _repository.AddAsync(request.Location);
        }
    }

}
