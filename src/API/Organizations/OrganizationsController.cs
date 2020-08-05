using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Common;
using API.Users;
using Core.Organizations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Organizations
{
    [ApiController]
    [Authorize]
    public class OrganizationsController : BaseController
    {
        private readonly IUserService _userService;
        private readonly ILogger<OrganizationsController> _logger;

        public OrganizationsController(IUserService userService, ILogger<OrganizationsController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpGet]
        [Route("/organizations")]
        public async Task<ActionResult<List<Organization>>> Get()
        {
            var user = await _userService.GetCurrentUser(HttpContext.User);
            return await Mediator.Send(new GetOrganizationsQuery { UserId = user.Id});
        }

        [HttpGet]
        [Route("/organizations/{id}")]
        public async Task<ActionResult<Organization>> GetById(int id)
        {
            var user = await _userService.GetCurrentUser(HttpContext.User);
            var organization = await Mediator.Send(new GetOrganizationByIdQuery { Id = id, UserId = user.Id });
            if (organization == null)
            {
                return NotFound();
            }

            return organization;
        }

        [HttpPost]
        [Route("/organizations")]
        public async Task<ActionResult<Organization>> Post(CreateOrganizationCommand body)
        {
            var user = await _userService.GetCurrentUser(HttpContext.User);
            body.CreatedBy = user.Id;
            body.OwnerId = user.Id;
            //body.CreatedUtc = DateTime.UtcNow;
            return await Mediator.Send(body);
        }

        [HttpPut]
        [Route("/organizations/{id}")]
        public async Task<ActionResult<Organization>> Put(int id, UpdateOrganizationCommand body)
        {
            var user = await _userService.GetCurrentUser(HttpContext.User);
            body.Organization.Id = id;
            body.Organization.ModifiedBy = user.Id;
            body.Organization.ModifiedUtc = DateTime.UtcNow;
            var organization = await Mediator.Send(body);
            if (organization == null)
            {
                return NotFound();
            }
            return organization;
        }
    }
}