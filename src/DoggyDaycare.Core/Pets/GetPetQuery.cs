using DoggyDaycare.Core.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DoggyDaycare.Core.Pets
{
    public class GetPetQuery : IRequest<Pet>
    {
        public string Id { get; set; }
    }

    public class GetPetQueryHandler : IRequestHandler<GetPetQuery, Pet>
    {
        private readonly IPetRepository _repository;

        public GetPetQueryHandler(IPetRepository repository)
        {
            _repository = repository;
        }

        public async Task<Pet> Handle(GetPetQuery request, CancellationToken cancellationToken)
        {
            return await _repository.FindAsync(request.Id);
        }
    }
}
