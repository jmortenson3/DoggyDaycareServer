using Core.Common;
using Core.Organizations;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Locations
{
    public class GetLocationByOrganizationQuery : IRequest<List<Location>>
    {
        public GetLocationByOrganizationQuery()
        {
        }

        public GetLocationByOrganizationQuery(int organizationId)
        {
            OrganizationId = organizationId;
        }

        public int OrganizationId { get; private set; }
    }

    public class GetLocationByOrganizationQueryHandler : IRequestHandler<GetLocationByOrganizationQuery, List<Location>>
    {
        private readonly ILocationRepository _locationRepository;
        private readonly IOrganizationRepository _organizationRepository;

        public GetLocationByOrganizationQueryHandler(ILocationRepository locationRepository, IOrganizationRepository organizationRepository)
        {
            _locationRepository = locationRepository;
            _organizationRepository = organizationRepository;
        }

        public async Task<List<Location>> Handle(GetLocationByOrganizationQuery request, CancellationToken cancellationToken)
        {
            var organization = await _organizationRepository.FindById(request.OrganizationId);
            return await _locationRepository.Find(location => location.OrganizationId == organization.Id);
        }
    }
}
