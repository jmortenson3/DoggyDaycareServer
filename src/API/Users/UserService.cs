using API.Exceptions;
using Common.Users;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace API.Users
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _config;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UserService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<ApplicationUser> Login(UserLoginModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Password))
            {
                throw new AppException("Password cannot be empty or whitespace.");
            }

            if (string.IsNullOrWhiteSpace(model.Email))
            {
                throw new AppException("Email cannot be empty or whitespace.");
            }

            var applicationUser = await GetUserByEmail(model.Email);

            if (applicationUser == null)
            {
                throw new AppException($"Cannot find user with email {model.Email}");
            }

            var result = await _signInManager.PasswordSignInAsync(applicationUser,
                model.Password, model.RememberMe, lockoutOnFailure: false);

            return applicationUser;
        }


        public async Task<ApplicationUser> Register(UserRegisterModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Password))
            {
                throw new AppException("Password cannot be empty or whitespace.");
            }

            if (string.IsNullOrWhiteSpace(model.Email))
            {
                throw new AppException("Email cannot be empty or whitespace.");
            }

            var applicationUser = new ApplicationUser
            {
                Email = model.Email,
                UserName = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName
            };

            var result = await _userManager.CreateAsync(applicationUser, model.Password);

            if (result.Succeeded)
            {
                return applicationUser;
            }

            string errors = "";
            foreach (var error in result.Errors)
            {
                errors += $"   {error.Description}";
            }

            throw new AppException($"Error(s) with registering user.  {errors}");
        }

        public async Task SignOut()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<ApplicationUser> GetCurrentUser(ClaimsPrincipal claim)
        {

            return await _userManager.GetUserAsync(claim);
        }

        public async Task<ApplicationUser> GetUserByEmail(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }
    }
}
