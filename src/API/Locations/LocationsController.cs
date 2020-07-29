using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Common;
using API.Users;
using Core.Locations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace API.Locations
{
    [ApiController]
    public class LocationsController : BaseController
    {
        private readonly IUserService _userService;

        public LocationsController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("/locations/{id}")]
        public async Task<ActionResult<Location>> GetById(int id)
        {
            return await Mediator.Send(new GetLocationQuery { Id = id });
        }

        [HttpGet]
        [Route("/locations")]
        public async Task<ActionResult<List<Location>>> Get([FromQuery(Name = "organization_id")] int organizationId)
        {
            var user = await _userService.GetCurrentUser(HttpContext.User);
            return await Mediator.Send(new GetLocationByOrganizationQuery { OrganizationId = organizationId, UserId = user.Id });
        }

        [HttpPost]
        [Route("/locations")]
        public async Task<ActionResult<Location>> Post(CreateLocationCommand body)
        {
            var user = await _userService.GetCurrentUser(HttpContext.User);
            body.CreatedBy = user.Id;
            body.CreatedUtc = DateTime.UtcNow;
            body.OwnerId = user.Id;
            return await Mediator.Send(body);
        }

        [HttpPut]
        [Route("/locations/{id}")]
        public async Task<ActionResult<Location>> Put(int id, UpdateLocationCommand body)
        {
            var user = await _userService.GetCurrentUser(HttpContext.User);
            body.Location.Id = id;
            body.Location.LastModifiedBy = user.Id;
            body.Location.LastModifiedUtc = DateTime.UtcNow;
            return await Mediator.Send(body);
        }

    }
}