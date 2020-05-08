using DoggyDaycare.Core.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DoggyDaycare.Core.Locations
{
    public class GetLocationQuery : IRequest<Location>
    {
        public int Id { get; set; }
    }

    public class GetLocationQueryHandler : IRequestHandler<GetLocationQuery, Location>
    {
        private readonly IAsyncRepository<Location> _repository;

        public GetLocationQueryHandler(IAsyncRepository<Location> repository)
        {
            _repository = repository;
        }

        public async Task<Location> Handle(GetLocationQuery request, CancellationToken cancellationToken)
        {
            return await _repository.FindAsync(request.Id);
        }
    }
}
