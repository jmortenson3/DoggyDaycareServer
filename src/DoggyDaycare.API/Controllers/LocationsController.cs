using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DoggyDaycare.Core.Locations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DoggyDaycare.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LocationsController : BaseController
    {

        [HttpGet("{id}")]
        public async Task<ActionResult<Location>> GetById(string id)
        {
            return await Mediator.Send(new GetLocationQuery { Id = id });
        }

        [HttpPost]
        public async Task<ActionResult<Location>> Post(Location location)
        {
            return await Mediator.Send(new CreateLocationCommand { Location = location });
        }


    }
}