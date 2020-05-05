using AutoMapper;
using DoggyDaycare.Core.Users;
using DoggyDaycare.Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoggyDaycare.API.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, ApplicationUser>();
        }
    }
}
