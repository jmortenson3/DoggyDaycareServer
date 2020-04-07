using DoggyDaycare.Core.Common;
using DoggyDaycare.Core.Locations.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DoggyDaycare.Core.Locations.Queries
{
    public class GetLocationQuery : IRequest<Location>
    {
        public string Id { get; set; }
    }

    public class GetLocationQueryHandler : IRequestHandler<GetLocationQuery, Location>
    {
        private readonly ILocationRepository _repository;

        public GetLocationQueryHandler(ILocationRepository repository)
        {
            _repository = repository;
        }

        public async Task<Location> Handle(GetLocationQuery request, CancellationToken cancellationToken)
        {
            return _repository.Find(request.Id);
        }
    }
}
