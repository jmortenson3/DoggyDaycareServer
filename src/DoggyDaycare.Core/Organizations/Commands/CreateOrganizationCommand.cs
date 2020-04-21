using DoggyDaycare.Core.Common;
using DoggyDaycare.Core.Organizations.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DoggyDaycare.Core.Organizations.Commands
{
    public class CreateOrganizationCommand : IRequest<Organization>
    {
        public Organization Organization { get; set; }
    }

    public class CreateOrganizationCommandHandler : IRequestHandler<CreateOrganizationCommand, Organization>
    {
        private readonly IOrganizationRepository _repository;

        public CreateOrganizationCommandHandler(IOrganizationRepository repository)
        {
            _repository = repository;
        }

        public async Task<Organization> Handle(CreateOrganizationCommand request, CancellationToken cancellationToken)
        {
            return await _repository.AddAsync(request.Organization);
        }
    }


}
