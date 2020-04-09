using DoggyDaycare.Core.Common;
using DoggyDaycare.Core.Pets.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DoggyDaycare.Core.Pets.Queries
{
    public class GetPetsByCustomerQuery : IRequest<List<Pet>>
    {
        public string CustomerId { get; set; }
    }

    public class GetPetsByCustomerQueryHandler : IRequestHandler<GetPetsByCustomerQuery, List<Pet>>
    {
        private readonly IPetRepository _repository;

        public GetPetsByCustomerQueryHandler(IPetRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Pet>> Handle(GetPetsByCustomerQuery request, CancellationToken cancellationToken)
        {
            return _repository.FindAll(p => p.CustomerId == request.CustomerId);
        }
    }
}
