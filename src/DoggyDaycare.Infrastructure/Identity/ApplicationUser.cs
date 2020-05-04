using DoggyDaycare.Core.Users;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DoggyDaycare.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser, IUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [JsonIgnore]
        public string Password { get; set; }
        public string Token { get; set; }
    }
}
