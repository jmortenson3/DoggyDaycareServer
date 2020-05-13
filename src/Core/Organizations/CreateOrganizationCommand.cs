using Core.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Organizations
{
    public class CreateOrganizationCommand : IRequest<Organization>
    {
        public CreateOrganizationCommand()
        {
        }

        public CreateOrganizationCommand(string ownerId, string name, string createdBy)
        {
            OwnerId = ownerId;
            Name = name;
            CreatedBy = createdBy;
        }
        
        public string OwnerId { get; private set; }
        public string Name { get; private set; }
        public DateTime CreatedUtc { get; private set; }
        public string CreatedBy { get; set; }
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
            var organization = new Organization
            {
                OwnerId = request.OwnerId,
                Name = request.Name,
                CreatedUtc = request.CreatedUtc,
                CreatedBy = request.CreatedBy
            };
            return await _repository.AddAsync(organization);
        }
    }


}
