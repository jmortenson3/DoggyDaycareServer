using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        [Route("/auth/signup")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApplicationUser>> Register(UserRegisterModel body)
        {
            var applicationUser = _mapper.Map<ApplicationUser>(body);
            await _userService.Register(applicationUser, body.Password);
            await _userService.Authenticate(body.Email, body.Password, body.RememberMe);
            return applicationUser;
        }

        [HttpPost]
        [Route("/auth/login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApplicationUser>> Authenticate(UserAuthenticateModel body)
        {
            var user = await _userService.Authenticate(body.Email, body.Password, body.RememberMe);
            return user;
        }

        [HttpGet]
        [Route("/users/me")]
        public async Task<ActionResult<ApplicationUser>> GetMe()
        {
            var user = await _userService.GetCurrentUser(HttpContext.User);
            return user;
        }
    }
}