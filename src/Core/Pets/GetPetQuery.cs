using Core.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Pets
{
    public class GetPetQuery : IRequest<Pet>
    {
        public int Id { get; set; }
    }

    public class GetPetQueryHandler : IRequestHandler<GetPetQuery, Pet>
    {
        private readonly IPetRepository _petRepository;

        public GetPetQueryHandler(IPetRepository petRepository)
        {
            _petRepository = petRepository;
        }

        public async Task<Pet> Handle(GetPetQuery request, CancellationToken cancellationToken)
        {
            return await _petRepository.FindById(request.Id);
        }
    }
}
