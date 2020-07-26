using Core.Common;
using Core.Memberships;
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
        public string OwnerId { get; set; }
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
        private readonly IMembershipRepository _membershipRepository;

        public CreateLocationCommandHandler(ILocationRepository locationRepository, IMembershipRepository membershipRepository)
        {
            _locationRepository = locationRepository;
            _membershipRepository = membershipRepository;
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

            await _locationRepository.Add(location);

            var membership = new Membership
            {
                IsMember = true,
                IsOwner = true,
                UserId = request.OwnerId,
                CreatedUtc = DateTime.UtcNow,
                CreatedBy = request.CreatedBy,
                Organization = location.Organization,
                Location = location
            };
            _membershipRepository.Add(membership);

            await _locationRepository.Save();
            return location;
        }
    }

}
