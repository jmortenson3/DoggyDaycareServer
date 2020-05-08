using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DoggyDaycare.API.Common;
using DoggyDaycare.Core.Pets;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DoggyDaycare.API.Pets
{
    [Route("[controller]")]
    [ApiController]
    public class PetsController : BaseController
    {

        [HttpGet("{id}")]
        public async Task<ActionResult<Pet>> GetById(int id)
        {
            return await Mediator.Send(new GetPetQuery { Id = id });
        }

        [HttpPost]
        public async Task<ActionResult<Pet>> Post(Pet pet)
        {
            return await Mediator.Send(new CreatePetCommand { Pet = pet });
        }
    }
}