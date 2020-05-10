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
        private readonly IAsyncRepository<Organization> _repostiory;

        public GetOrganizationQueryHandler(IAsyncRepository<Organization> repository)
        {
            _repostiory = repository;
        }

        public async Task<Organization> Handle(GetOrganizationQuery request, CancellationToken cancellationToken)
        {
            var organization = await _repostiory.FindAsync(request.Id);
            return organization;
        }
    }
}
