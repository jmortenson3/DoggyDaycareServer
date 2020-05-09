using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DoggyDaycare.API.Common;
using DoggyDaycare.API.Users;
using DoggyDaycare.Core.Locations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DoggyDaycare.API.Locations
{
    [Route("[controller]")]
    [ApiController]
    public class LocationsController : BaseController
    {
        private readonly IUserService _userService;

        public LocationsController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Location>> GetById(int id)
        {
            return await Mediator.Send(new GetLocationQuery { Id = id });
        }

        [HttpPost]
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