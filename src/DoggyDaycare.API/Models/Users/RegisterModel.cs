using AutoMapper;
using DoggyDaycare.Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DoggyDaycare.API.Models.Users
{
    public class RegisterModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        public class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<RegisterModel, ApplicationUser>()
                    .ForMember(dest =>
                        dest.UserName,
                        opt => opt.MapFrom(src => src.Email)
                    );
            }

        }
    }
}
