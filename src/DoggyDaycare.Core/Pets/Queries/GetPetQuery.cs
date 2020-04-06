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
            return _repository.Find(request.Id);
        }
    }
}
                                                                                                                