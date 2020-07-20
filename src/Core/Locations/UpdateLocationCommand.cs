﻿using Core.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Locations
{
    public class UpdateLocationCommand : IRequest<Location>
    {
        public Location Location { get; set; }
    }

    public class UpdateLocationCommandHandler : IRequestHandler<UpdateLocationCommand, Location>
    {
        private readonly ILocationRepository _locationRepository;

        public UpdateLocationCommandHandler(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public async Task<Location> Handle(UpdateLocationCommand request, CancellationToken cancellationToken)
        {
            return await _locationRepository.Update(request.Location);
        }
    }
}