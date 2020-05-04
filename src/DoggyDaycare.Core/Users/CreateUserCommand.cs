using DoggyDaycare.Core.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DoggyDaycare.Core.Users
{
    public class CreateUserCommand : IRequest<User>
    {
        public User User { get; set; }
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, User>
    {
        private readonly IAsyncRepository<User> _repository;
        public CreateUserCommandHandler(IAsyncRepository<User> repository)
        {
            _repository = repository;
        }

        public async Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            return await _repository.AddAsync(request.User);
        }
    }
}
