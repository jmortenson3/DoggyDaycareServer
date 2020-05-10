using Core.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Pets
{
    public class CreatePetCommand : IRequest<Pet>
    {
        public Pet Pet { get; set; }
    }

    public class CreatePetCommandHandler : IRequestHandler<CreatePetCommand, Pet>
    {

        private readonly IAsyncRepository<Pet> _repository;

        public CreatePetCommandHandler(IAsyncRepository<Pet> repository)
        {
            _repository = repository;
        }

        public async Task<Pet> Handle(CreatePetCommand request, CancellationToken cancellationToken)
        {
            return await _repository.AddAsync(request.Pet);
        }
    }
}
