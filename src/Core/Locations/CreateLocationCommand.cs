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
        public string UserId { get; set; }
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
        private readonly IOrganizationRepository _organizationRepository;

        public CreateLocationCommandHandler(
            ILocationRepository locationRepository, 
            IMembershipRepository membershipRepository,
            IOrganizationRepository organizationRepository)
        {
            _locationRepository = locationRepository;
            _membershipRepository = membershipRepository;
            _organizationRepository = organizationRepository;
        }

        public async Task<Location> Handle(CreateLocationCommand request, CancellationToken cancellationToken)
        {

            var organization = _organizationRepository.Find(request.OrganizationId, request.UserId);

            if (organization == null)
            {
                return null;
            }

            var location = new Location
            {
                OrganizationId = organization.Id,
                Name = request.Name,
                CreatedBy = request.CreatedBy,
                CreatedUtc = request.CreatedUtc
            };

            if (organization.Locations == null)
            {
                organization.Locations = new List<Location>();
            }

            _locationRepository.Add(location);
            organization.Locations.Add(location);
            location.Organization = organization;

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

            await _locationRepository.SaveAsync();
            return location;
        }
    }

}
