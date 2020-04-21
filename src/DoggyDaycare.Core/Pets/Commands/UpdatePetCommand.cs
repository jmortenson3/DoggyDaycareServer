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
    public class UpdatePetCommand : IRequest<Pet>
    {
        public Pet Pet { get; set; }
    }

    public class UpdatePetCommandHandler : IRequestHandler<UpdatePetCommand, Pet>
    {
        private readonly IPetRepository _repository;
        public UpdatePetCommandHandler(IPetRepository repository)
        {
            _repository = repository;
        }

        public async Task<Pet> Handle(UpdatePetCommand request, CancellationToken cancellationToken)
        {
            var pet = await _repository.UpdateAsync(request.Pet);
            return pet;
        }
    }
}
