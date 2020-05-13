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
        public GetPetQuery()
        {
        }

        public GetPetQuery(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }
    }

    public class GetPetQueryHandler : IRequestHandler<GetPetQuery, Pet>
    {
        private readonly IAsyncRepository<Pet> _repository;

        public GetPetQueryHandler(IAsyncRepository<Pet> repository)
        {
            _repository = repository;
        }

        public async Task<Pet> Handle(GetPetQuery request, CancellationToken cancellationToken)
        {
            return await _repository.FindAsync(request.Id);
        }
    }
}
