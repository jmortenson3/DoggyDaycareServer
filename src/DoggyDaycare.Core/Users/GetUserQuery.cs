using DoggyDaycare.Core.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DoggyDaycare.Core.Users
{
    public class GetUserQuery : IRequest<User>
    {
        public string Id { get; set; }
    }

    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, User>
    {
        private readonly IAsyncRepository<User> _repository;

        public GetUserQueryHandler(IAsyncRepository<User> repository)
        {
            _repository = repository;
        }

        public async Task<User> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _repository.FindAsync(request.Id);
            return user;
        }
    }
}
