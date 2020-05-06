using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DoggyDaycare.API.Models.Users;
using DoggyDaycare.API.Services;
using DoggyDaycare.Infrastructure.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DoggyDaycare.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }


        [HttpPost]
        [Route("signup")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApplicationUser>> Register(RegisterModel body)
        {
            var applicationUser = _mapper.Map<ApplicationUser>(body);
            await _userService.Register(applicationUser, body.Password);
            return applicationUser;
        }

        [HttpPost]
        [Route("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApplicationUser>> Authenticate(AuthenticateModel body)
        {
            var user = await _userService.Authenticate(body.Email, body.Password, body.RememberMe);
            return user;
        }
    }
}