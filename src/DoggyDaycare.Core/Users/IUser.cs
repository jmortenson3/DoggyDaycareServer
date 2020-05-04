using System;
using System.Collections.Generic;
using System.Text;

namespace DoggyDaycare.Core.Users
{
    public interface IUser
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
