﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DoggyDaycare.API.Models;
using DoggyDaycare.API.Services;
using DoggyDaycare.Core.Organizations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DoggyDaycare.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class OrganizationsController : BaseController
    {
        private readonly IUserService _userService;
        public OrganizationsController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Organization>> GetById(string id)
        {
            return await Mediator.Send(new GetOrganizationQuery { Id = id });
        }

        [HttpPost]
        public async Task<ActionResult<Organization>> Post(CreateOrganizationModel body)
        {
            var user = await _userService.GetCurrentUser(HttpContext.User);
            var organization = new Organization
            {
                // set ID to a GUID
                Name = body.Name,
                CreatedBy = user.Id,
                OwnerId = "1" //user.Id
            };
            return await Mediator.Send(
                new CreateOrganizationCommand { Organization = organization });
        }
    }
}