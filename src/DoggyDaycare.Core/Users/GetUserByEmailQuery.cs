using DoggyDaycare.Core.Common;
using DoggyDaycare.Core.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DoggyDaycare.Core.Users
{
    public class GetUserByEmailQuery : IRequest<User>
    {
        public string Email { get; set; }
    }

    public class GetUserByEmailQueryHandler : IRequestHandler<GetUserByEmailQuery, User>
    {
        private readonly IAsyncRepository<User> _repository;

        public GetUserByEmailQueryHandler(IAsyncRepository<User> repository)
        {
            _repository = repository;
        }

        public async Task<User> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            var users = await _repository.FindAllAsync(user => user.Email == request.Email);

            if (users.Count == 1)
            {
                return users[0];
            }
            else if (users.Count > 1)
            {
                string message = $"Multiple users found when search for users with email ${request.Email}";
                throw new MultipleUsersWithSameEmailException(message);
            } 
            else
            {
                string message = $"User with email ${request.Email} was not found";
                throw new UserNotFoundException(message);
            }
        }
    }
}
