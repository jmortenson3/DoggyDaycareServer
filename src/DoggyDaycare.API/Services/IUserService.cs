﻿using DoggyDaycare.API.Models.Users;
using DoggyDaycare.Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoggyDaycare.API.Services
{
    public interface IUserService
    {
        Task<ApplicationUser> Authenticate(string email, string password, bool rememberMe);
        Task<ApplicationUser> Register(ApplicationUser user, string password);
        Task SignOut();
    }
}
