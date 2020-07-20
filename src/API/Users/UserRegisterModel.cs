using AutoMapper;
using Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Users
{
    public class UserRegisterModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public bool RememberMe { get; set; }

        public class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<UserRegisterModel, ApplicationUser>()
                    .ForMember(dest =>
                        dest.UserName,
                        opt => opt.MapFrom(src => src.Email)
                    );
            }

        }
    }
}
