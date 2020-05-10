using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Common;
using API.Users;
using Core.Organizations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Organizations
{
    [ApiController]
    [Authorize]
    public class OrganizationsController : BaseController
    {
        private readonly IUserService _userService;

        public OrganizationsController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("/organizations/{id}")]
        public async Task<ActionResult<Organization>> GetById(int id)
        {
            return await Mediator.Send(new GetOrganizationQuery { Id = id });
        }

        [HttpPost]
        [Route("/organizations")]
        public async Task<ActionResult<Organization>> Post(CreateOrganizationModel body)
        {
            var user = await _userService.GetCurrentUser(HttpContext.User);
            
            var organization = new Organization
            {
                Name = body.Name,
                CreatedBy = user.Id,
                OwnerId = user.Id
            };

            return await Mediator.Send(
                new CreateOrganizationCommand { Organization = organization });
        }
    }
}