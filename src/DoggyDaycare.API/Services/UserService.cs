using DoggyDaycare.Core.Users;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoggyDaycare.API.Services
{
    public class UserService : IUserService
    {
        private readonly IMediator _mediator;
        public UserService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public User Authenticate(string email, string password)
        {
            var query = new GetUserByEmailQuery { Email = email };
            var users = _mediator.Send(query);

            return null;
        }
    }
}
