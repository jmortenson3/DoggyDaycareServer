using Core.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Organizations
{
    public class GetOrganizationQuery : IRequest<Organization>
    {
        public int Id { get; set; }
    }

    public class GetOrganizationQueryHandler : IRequestHandler<GetOrganizationQuery, Organization>
    {
        private readonly IOrganizationRepository _organizationRepository;

        public GetOrganizationQueryHandler(IOrganizationRepository organizationRepository)
        {
            _organizationRepository = organizationRepository;
        }

        public async Task<Organization> Handle(GetOrganizationQuery request, CancellationToken cancellationToken)
        {
            return await _organizationRepository.FindById(request.Id);
        }
    }
}
