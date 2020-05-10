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

        [HttpPost]
        [Route("/locations")]
        public async Task<ActionResult<Location>> Post(CreateLocationModel body)
        {
            var user = await _userService.GetCurrentUser(HttpContext.User);

            var location = new Location
            {
                OrganizationId = body.OrganizationId,
                Name = body.Name,
                CreatedBy = user.Id
            };

            return await Mediator.Send(new CreateLocationCommand { Location = location });
        }


    }
}