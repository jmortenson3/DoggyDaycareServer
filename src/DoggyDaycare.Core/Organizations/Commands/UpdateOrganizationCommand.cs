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
    public class UpdateOrganizationCommand : IRequest<Organization>
    {
        public Organization Organization { get; set; }
    }

    public class UpdateOrganizationCommandHandler : IRequestHandler<UpdateOrganizationCommand, Organization>
    {
        private readonly IOrganizationRepository _repository;

        public UpdateOrganizationCommandHandler(IOrganizationRepository repository)
        {
            _repository = repository;
        }

        public async Task<Organization> Handle(UpdateOrganizationCommand request, CancellationToken cancellationToken)
        {
            return _repository.Update(request.Organization);
        }
    }
}
