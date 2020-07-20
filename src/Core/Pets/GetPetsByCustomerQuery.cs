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
        public string OwnerId { get; set; }
    }

    public class GetPetsByCustomerQueryHandler : IRequestHandler<GetPetsByCustomerQuery, List<Pet>>
    {
        private readonly IPetRepository _petRepository;

        public GetPetsByCustomerQueryHandler(IPetRepository petRepository)
        {
            _petRepository = petRepository;
        }

        public async Task<List<Pet>> Handle(GetPetsByCustomerQuery request, CancellationToken cancellationToken)
        {
            return await _petRepository.Find(p => p.OwnerId == request.OwnerId);
        }
    }
}
