using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DoggyDaycare.Core.Organizations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DoggyDaycare.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationsController : BaseController
    {

        [HttpGet("{id}")]
        public async Task<ActionResult<Organization>> GetById(string id)
        {
            return await Mediator.Send(new GetOrganizationQuery { Id = id });
        }

        [HttpPost]
        public async Task<ActionResult<Organization>> Post(Organization organization)
        {
            return await Mediator.Send(
                new CreateOrganizationCommand { Organization = organization });
        }
    }
}