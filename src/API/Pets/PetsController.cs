using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Common;
using API.Users;
using Core.Pets;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Pets
{
    [ApiController]
    public class PetsController : BaseController
    {
        private readonly IUserService _userService;

        public PetsController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("/pets/{id}")]
        public async Task<ActionResult<Pet>> GetById(int id)
        {
            return await Mediator.Send(new GetPetQuery { Id = id });
        }

        [HttpGet]
        [Route("/pets")]
        public async Task<ActionResult<List<Pet>>> Get(
            [FromQuery(Name = "user_id")] string? userId, 
            [FromQuery(Name = "organization_id")] int? organizationId,
            [FromQuery(Name = "location_id")] int? locationId)
        {
            return await Mediator.Send(new GetPetsByOwnerQuery { OwnerId = userId });
        }

        [HttpPost]
        [Route("/pets")]
        public async Task<ActionResult<Pet>> Post(CreatePetCommand body)
        {
            var user = await _userService.GetCurrentUser(HttpContext.User);
            body.CreatedBy = user.Id;
            return await Mediator.Send(body);
        }

        [HttpPut]
        [Route("/pets/{id}")]
        public async Task<ActionResult<Pet>> Put(int id, UpdatePetCommand body)
        {
            var user = await _userService.GetCurrentUser(HttpContext.User);
            body.Pet.Id = id;
            body.Pet.ModifiedBy = user.Id;
            body.Pet.ModifiedUtc = DateTime.UtcNow;
            return await Mediator.Send(body);
        }
    }
}