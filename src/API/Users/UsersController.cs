using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace API.Users
{
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
        [Route("/users/signup")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApplicationUser>> Register(UserRegisterModel body)
        {
            var applicationUser = _mapper.Map<ApplicationUser>(body);
            await _userService.Register(applicationUser, body.Password);
            return applicationUser;
        }

        [HttpPost]
        [Route("/users/login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApplicationUser>> Authenticate(UserAuthenticateModel body)
        {
            var user = await _userService.Authenticate(body.Email, body.Password, body.RememberMe);
            return user;
        }

        [HttpPost]
        [Route("/users/me")]
        public async Task<ActionResult<string>> Me()
        {
            var user = await _userService.GetCurrentUser(HttpContext.User);
            return JsonConvert.SerializeObject(user);
        }
    }
}