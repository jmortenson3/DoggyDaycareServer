using Common.Users;
using Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace API.Users
{
    public interface IUserService
    {
        Task<ApplicationUser> Login(UserLoginModel model);
        Task<ApplicationUser> Register(UserRegisterModel model);
        Task SignOut();
        Task<ApplicationUser> GetCurrentUser(ClaimsPrincipal claim);
        Task<ApplicationUser> GetUserByEmail(string email);
    }
}
