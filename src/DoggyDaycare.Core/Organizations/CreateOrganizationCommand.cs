using DoggyDaycare.Core.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DoggyDaycare.Core.Organizations
{
    public class CreateOrganizationCommand : IRequest<Organization>
    {
        public Organization Organization { get; set; }
    }

    public class CreateOrganizationCommandHandler : IRequestHandler<CreateOrganizationCommand, Organization>
    {
        private readonly IAsyncRepository<Organization> _repository;

        public CreateOrganizationCommandHandler(IAsyncRepository<Organization> repository)
        {
            _repository = repository;
        }

        public async Task<Organization> Handle(CreateOrganizationCommand request, CancellationToken cancellationToken)
        {
            request.Organization.CreatedUtc = DateTime.Now;
            return await _repository.AddAsync(request.Organization);
        }
    }


}
