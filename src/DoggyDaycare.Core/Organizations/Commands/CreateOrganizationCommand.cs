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
    public class CreateOrganizationCommand : IRequest<string>
    {
        public Organization Organization { get; set; }
    }

    public class CreateOrganizationCommandHandler : IRequestHandler<CreateOrganizationCommand, string>
    {
        private readonly IOrganizationRepository _repository;

        public CreateOrganizationCommandHandler(IOrganizationRepository repository)
        {
            _repository = repository;
        }

        public async Task<string> Handle(CreateOrganizationCommand request, CancellationToken cancellationToken)
        {
            var id = _repository.Add(request.Organization);
            return id;
        }
    }


}
