using Core.Common;
using Core.Organizations;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Locations
{
    public class CreateLocationCommand : IRequest<Location>
    {
        [Required]
        public int OrganizationId { get; set; }
        [Required]
        public string Name { get; set; }
        [JsonIgnore]
        public DateTime CreatedUtc { get; set; }
        [JsonIgnore]
        public string CreatedBy { get; set; }
    }

    public class CreateLocationCommandHandler : IRequestHandler<CreateLocationCommand, Location>
    {
        private readonly ILocationRepository _locationRepository;

        public CreateLocationCommandHandler(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
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

            return await _locationRepository.Add(location);
        }
    }

}
