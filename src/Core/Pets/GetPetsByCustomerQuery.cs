using Core.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Pets
{
    public class GetPetsByCustomerQuery : IRequest<List<Pet>>
    {
        public GetPetsByCustomerQuery()
        {
        }

        public GetPetsByCustomerQuery(string ownerId)
        {
            OwnerId = ownerId;
        }

        public string OwnerId { get; private set; }
    }

    public class GetPetsByCustomerQueryHandler : IRequestHandler<GetPetsByCustomerQuery, List<Pet>>
    {
        private readonly IAsyncRepository<Pet> _repository;

        public GetPetsByCustomerQueryHandler(IAsyncRepository<Pet> repository)
        {
            _repository = repository;
        }

        public async Task<List<Pet>> Handle(GetPetsByCustomerQuery request, CancellationToken cancellationToken)
        {
            return await _repository.FindAllAsync(p => p.OwnerId == request.OwnerId);
        }
    }
}
