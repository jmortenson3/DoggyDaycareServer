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
    [Route("[controller]")]
    [ApiController]
    public class PetsController : BaseController
    {
        private readonly IUserService _userService;

        public PetsController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pet>> GetById(int id)
        {
            return await Mediator.Send(new GetPetQuery(id));
        }

        [HttpPost]
        public async Task<ActionResult<Pet>> Post(CreatePetCommand body)
        {
            var user = await _userService.GetCurrentUser(HttpContext.User);
            body.CreatedBy = user.Id;
            return await Mediator.Send(body);
        }
    }
}