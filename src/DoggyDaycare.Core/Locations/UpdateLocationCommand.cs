using DoggyDaycare.Core.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DoggyDaycare.Core.Locations
{
    public class UpdateLocationCommand : IRequest<Location>
    {
        public Location Location { get; set; }
    }

    public class UpdateLocationCommandHandler : IRequestHandler<UpdateLocationCommand, Location>
    {
        private readonly ILocationRepository _repository;

        public UpdateLocationCommandHandler(ILocationRepository repository)
        {
            _repository = repository;
        }

        public async Task<Location> Handle(UpdateLocationCommand request, CancellationToken cancellationToken)
        {
            return await _repository.UpdateAsync(request.Location);
        }
    }
}