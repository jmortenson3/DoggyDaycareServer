﻿using Core.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Organizations
{
    public class UpdateOrganizationCommand : IRequest<Organization>
    {
        public Organization Organization { get; set; }
    }

    public class UpdateOrganizationCommandHandler : IRequestHandler<UpdateOrganizationCommand, Organization>
    {
        private readonly IOrganizationRepository _organizationRepository;

        public UpdateOrganizationCommandHandler(IOrganizationRepository organizationRepository)
        {
            _organizationRepository = organizationRepository;
        }

        public async Task<Organization> Handle(UpdateOrganizationCommand request, CancellationToken cancellationToken)
        {
            var organization = await _organizationRepository.Find(request.Organization.Id);
            organization.Name = request.Organization.Name;
            await _organizationRepository.Save();
            return organization;
        }
    }
}
