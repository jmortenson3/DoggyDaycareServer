using DoggyDaycare.Core.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DoggyDaycare.Core.Users
{
    public class UpdateUserCommand : IRequest<User>
    {
        public User User { get; set; }

    }

    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, User>
    {
        private readonly IAsyncRepository<User> _repository;

        public UpdateUserCommandHandler(IAsyncRepository<User> repository)
        {
            _repository = repository;
        }

        public async Task<User> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            return await _repository.UpdateAsync(request.User);

        }
    }
}
