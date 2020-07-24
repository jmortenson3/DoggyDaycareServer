using Core.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Organizations
{
    public class GetOrganizationByIdQuery : IRequest<Organization>
    {
        public int Id { get; set; }
    }

    public class GetOrganizationByIdQueryHandler : IRequestHandler<GetOrganizationByIdQuery, Organization>
    {
        private readonly IOrganizationRepository _organizationRepository;

        public GetOrganizationByIdQueryHandler(IOrganizationRepository organizationRepository)
        {
            _organizationRepository = organizationRepository;
        }

        public async Task<Organization> Handle(GetOrganizationByIdQuery request, CancellationToken cancellationToken)
        {
            return await _organizationRepository.FindById(request.Id);
        }
    }
}
