using AutoMapper;
using DoggyDaycare.Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DoggyDaycare.API.Models.Users
{
    public class UserAuthenticateModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public bool RememberMe { get; set; }

    }
}
