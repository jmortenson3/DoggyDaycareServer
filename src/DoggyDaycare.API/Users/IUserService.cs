using DoggyDaycare.Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DoggyDaycare.API.Users
{
    public interface IUserService
    {
        Task<ApplicationUser> Authenticate(string email, string password, bool rememberMe);
        Task<ApplicationUser> Register(ApplicationUser user, string password);
        Task SignOut();
        Task<ApplicationUser> GetCurrentUser(ClaimsPrincipal claim);
        Task<ApplicationUser> GetUserByEmail(string email);
    }
}
