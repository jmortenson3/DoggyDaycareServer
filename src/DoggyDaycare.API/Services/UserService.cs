using AutoMapper;
using DoggyDaycare.API.Exceptions;
using DoggyDaycare.Infrastructure.Identity;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DoggyDaycare.API.Services
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _config;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UserService(
            IConfiguration config, 
            UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager)
        {
            _config = config;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<ApplicationUser> Authenticate(string email, string password, bool rememberMe)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new AppException("Password cannot be empty or whitespace.");
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                throw new AppException("Email cannot be empty or whitespace.");
            }

            var applicationUser = await _userManager.Users.SingleOrDefaultAsync(u => u.Email.ToUpper() == email.ToUpper());

            if (applicationUser == null)
            {
                throw new AppException($"Cannot find user with email {email}");
            }

            var result = await _signInManager.PasswordSignInAsync(applicationUser, 
                password, rememberMe, lockoutOnFailure: false);

            return applicationUser;
        }


        public async Task<ApplicationUser> Register(ApplicationUser user, string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new AppException("Password cannot be empty or whitespace.");
            }

            if (string.IsNullOrWhiteSpace(user.Email))
            {
                throw new AppException("Email cannot be empty or whitespace.");
            }

            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                return user;
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
    }
}
