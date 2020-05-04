﻿using DoggyDaycare.Core.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoggyDaycare.API.Services
{
    public interface IUserService
    {
        User Authenticate(string email, string password);
    }
}
