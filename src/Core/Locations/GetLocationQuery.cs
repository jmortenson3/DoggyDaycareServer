using Core.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Locations
{
    public class GetLocationQuery : IRequest<Location>
    {
        public GetLocationQuery()
        {
        }

        public GetLocationQuery(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }
    }

    public class GetLocationQueryHandler : IRequestHandler<GetLocationQuery, Location>
    {
        private readonly ILocationRepository _locationRepository;

        public GetLocationQueryHandler(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public async Task<Location> Handle(GetLocationQuery request, CancellationToken cancellationToken)
        {
            return await _locationRepository.FindById(request.Id);
        }
    }
}
