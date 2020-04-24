using DoggyDaycare.Core.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DoggyDaycare.Core.Pets
{
    public class GetPetsByCustomerQuery : IRequest<List<Pet>>
    {
        public string CustomerId { get; set; }
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
            return await _repository.FindAllAsync(p => p.CustomerId == request.CustomerId);
        }
    }
}
