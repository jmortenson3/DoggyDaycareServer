using Core.Common;
using Core.Memberships;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Organizations
{
    public class CreateOrganizationCommand : IRequest<Organization>
    {
        [JsonIgnore]
        public string OwnerId { get; set; }
        [Required]
        public string Name { get; set; }
        [JsonIgnore]
        public DateTime CreatedUtc { get; set; }
        [JsonIgnore]
        public string CreatedBy { get; set; }
    }

    public class CreateOrganizationCommandHandler : IRequestHandler<CreateOrganizationCommand, Organization>
    {
        private readonly IOrganizationRepository _organizationRepository;
        private readonly IMembershipRepository _membershipRepository;

        public CreateOrganizationCommandHandler(IOrganizationRepository organizationRepository, IMembershipRepository membershipRepository)
        {
            _organizationRepository = organizationRepository;
            _membershipRepository = membershipRepository;
        }

        public async Task<Organization> Handle(CreateOrganizationCommand request, CancellationToken cancellationToken)
        {
            var organization = new Organization
            {
                OwnerId = request.OwnerId,
                Name = request.Name,
                CreatedUtc = request.CreatedUtc,
                CreatedBy = request.CreatedBy
            };
            _organizationRepository.Add(organization);

            var membership = new Membership
            {
                IsMember = true,
                IsOwner = true,
                UserId = request.OwnerId,
                CreatedUtc = DateTime.UtcNow,
                CreatedBy = request.CreatedBy,
                OrganizationId = organization.Id,
                Organization = organization
            };
            _membershipRepository.Add(membership);

            await _organizationRepository.Save();
            return organization;
        }
    }


}
