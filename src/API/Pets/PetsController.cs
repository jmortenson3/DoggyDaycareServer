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
            return await Mediator.Send(new GetPetQuery { Id = id });
        }

        [HttpPost]
        public async Task<ActionResult<Pet>> Post(Pet body)
        {
            var pet = body;
            pet.CreatedBy = (await _userService.GetCurrentUser(HttpContext.User)).Id;
            return await Mediator.Send(new CreatePetCommand { Pet = pet });
        }
    }
}