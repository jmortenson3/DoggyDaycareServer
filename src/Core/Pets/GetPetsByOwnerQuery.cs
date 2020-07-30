using Core.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Pets
{
    public class GetPetsByOwnerQuery : IRequest<List<Pet>>
    {
        public string OwnerId { get; set; }
    }

    public class GetPetsByOwnerQueryHandler : IRequestHandler<GetPetsByOwnerQuery, List<Pet>>
    {
        private readonly IPetRepository _petRepository;

        public GetPetsByOwnerQueryHandler(IPetRepository petRepository)
        {
            _petRepository = petRepository;
        }

        public async Task<List<Pet>> Handle(GetPetsByOwnerQuery request, CancellationToken cancellationToken)
        {
            return await _petRepository.FindByOwner(request.OwnerId);
        }
    }
}
