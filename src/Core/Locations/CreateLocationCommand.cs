using Core.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Locations
{
    public class CreateLocationCommand : IRequest<Location>
    {
        public CreateLocationCommand()
        {
        }

        public CreateLocationCommand(int organizationId, string name, DateTime createdUtc)
        {
            OrganizationId = organizationId;
            Name = name;
            CreatedUtc = createdUtc;
        }

        public int OrganizationId { get; private set; }
        public string Name { get; private set; }
        public DateTime CreatedUtc { get; private set; }
        public string CreatedBy { get; set; }
    }

    public class CreateLocationCommandHandler : IRequestHandler<CreateLocationCommand, Location>
    {
        private readonly IAsyncRepository<Location> _repository;

        public CreateLocationCommandHandler(IAsyncRepository<Location> repository)
        {
            _repository = repository;
        }

        public async Task<Location> Handle(CreateLocationCommand request, CancellationToken cancellationToken)
        {
            var location = new Location
            {
                OrganizationId = request.OrganizationId,
                Name = request.Name,
                CreatedBy = request.CreatedBy,
                CreatedUtc = request.CreatedUtc
            };

            return await _repository.AddAsync(location);
        }
    }

}
