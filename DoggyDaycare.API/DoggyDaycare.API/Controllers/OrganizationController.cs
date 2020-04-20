using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DoggyDaycare.Core.Organizations.Commands;
using DoggyDaycare.Core.Organizations.Entities;
using DoggyDaycare.Core.Organizations.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DoggyDaycare.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationController : BaseController
    {
        private static readonly List<string> DefaultOrganizations = new List<string>
        {
            "Organization0",
            "Organization1",
            "Organization2"
        };

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return DefaultOrganizations;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Organization>> GetById(string id)
        {
            var query = new GetOrganizationQuery
            {
                Id = id
            };

            var result = await Mediator.Send(query);
            return result;
        }

        [HttpPost]
        public async Task<ActionResult<string>> Post(Organization organization)
        {
            var command = new CreateOrganizationCommand
            {
                Organization = organization
            };
            var result = await Mediator.Send(command);
            return result;
        }
    }
}