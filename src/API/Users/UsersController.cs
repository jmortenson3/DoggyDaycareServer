using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Common.Users;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            //var applicationUser = _mapper.Map<ApplicationUser>(body);
            await _userService.Register(body);
            var userLoginModel = new UserLoginModel { Email = body.Email, Password = body.Password, RememberMe = body.RememberMe };
            var user = await _userService.Login(userLoginModel);
            return user;
        }

        [HttpPost]
        [Route("/auth/login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApplicationUser>> Authenticate(UserLoginModel body)
        {
            var user = await _userService.Login(body);
            return user;
        }

        [HttpGet]
        [Route("/users/me")]
        [Authorize]
        public async Task<ActionResult<ApplicationUser>> GetMe()
        {
            var user = await _userService.GetCurrentUser(HttpContext.User);
            return user;
        }
    }
}