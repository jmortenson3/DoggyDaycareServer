using DoggyDaycare.Core.Common;
using DoggyDaycare.Core.Pets.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DoggyDaycare.Core.Pets.Commands
{
    public class CreatePetCommand : IRequest<Pet>
    {
        public Pet Pet { get; set; }
    }

    public class CreatePetCommandHandler : IRequestHandler<CreatePetCommand, Pet>
    {

        private readonly IPetRepository _repository;

        public CreatePetCommandHandler(IPetRepository repository)
        {
            _repository = repository;
        }

        public async Task<Pet> Handle(CreatePetCommand request, CancellationToken cancellationToken)
        {
            return await _repository.AddAsync(request.Pet);
        }
    }
}
