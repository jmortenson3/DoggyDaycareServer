using DoggyDaycare.Core.Common;
using DoggyDaycare.Core.Organizations.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DoggyDaycare.Core.Organizations.Queries
{
    public class GetOrganizationQuery : IRequest<Organization>
    {
        public string Id { get; set; }
    }

    public class GetOrganizationQueryHandler : IRequestHandler<GetOrganizationQuery, Organization>
    {
        private readonly IOrganizationRepository _repostiory;

        public GetOrganizationQueryHandler(IOrganizationRepository repository)
        {
            _repostiory = repository;
        }

        public async Task<Organization> Handle(GetOrganizationQuery request, CancellationToken cancellationToken)
        {
            var organization = _repostiory.Find(request.Id);
            return organization;
        }
    }
}
