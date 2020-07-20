using Core.Common;
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

        public CreateOrganizationCommandHandler(IOrganizationRepository organizationRepository)
        {
            _organizationRepository = organizationRepository;
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
            return await _organizationRepository.Add(organization);
        }
    }


}
