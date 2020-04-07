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
    public class CreatePetCommand : IRequest<string>
    {
        public Pet Pet { get; set; }
    }

    public class CreatePetCommandHandler : IRequestHandler<CreatePetCommand, string>
    {

        private readonly IPetRepository _repository;

        public CreatePetCommandHandler(IPetRepository repository)
        {
            _repository = repository;
        }

        public async Task<string> Handle(CreatePetCommand request, CancellationToken cancellationToken)
        {
            _repository.Add(request.Pet);

            return request.Pet.Id;
        }
    }
}
