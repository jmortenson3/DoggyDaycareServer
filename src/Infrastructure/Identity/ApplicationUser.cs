using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Token { get; set; }

        //[JsonIgnore]
        public string PasswordSalt { get; set; }
        //[JsonIgnore]
        public override string PasswordHash { get; set; }
        [JsonIgnore]
        public override bool TwoFactorEnabled { get; set; }
        //[JsonIgnore]
        public override string NormalizedEmail { get; set; }
        //[JsonIgnore]
        public override string NormalizedUserName { get; set; }
        [JsonIgnore]
        public override int AccessFailedCount { get; set; }
        [JsonIgnore]
        public override string ConcurrencyStamp { get; set; }
        //[JsonIgnore]
        public override string UserName { get; set; }
    }
}
