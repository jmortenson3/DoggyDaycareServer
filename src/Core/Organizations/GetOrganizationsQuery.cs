using Core.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Organizations
{
    public class GetOrganizationsQuery : IRequest<List<Organization>>
    {
        public string UserId { get; set; }
    }

    public class GetOrganizationsQueryHandler : IRequestHandler<GetOrganizationsQuery, List<Organization>>
    {
        private readonly IOrganizationRepository _organizationRepository;

        public GetOrganizationsQueryHandler(IOrganizationRepository organizationRepository)
        {
            _organizationRepository = organizationRepository;
        }

        public async Task<List<Organization>> Handle(GetOrganizationsQuery request, CancellationToken cancellationToken)
        {
            return await _organizationRepository.FindAllAsync(request.UserId);
        }
    }
}
