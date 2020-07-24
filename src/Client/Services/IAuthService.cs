using Common.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Services
{
    public interface IAuthService
    {
        Task Login();
        Task Register(UserRegisterModel model);
        Task Logout();
        Task GetCurrentUser();
    }
}
