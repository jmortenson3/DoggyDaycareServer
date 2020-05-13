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
        public CreatePetCommand()
        {
        }

        public CreatePetCommand(string ownerId, string name, DateTime createdUtc)
        {
            OwnerId = ownerId;
            Name = name;
            CreatedUtc = createdUtc;
        }

        public string OwnerId { get; private set; }
        public string Name { get; private set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedUtc { get; private set; }
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
            var pet = new Pet
            {
                OwnerId = request.OwnerId,
                Name = request.Name,
                CreatedBy = request.CreatedBy,
                CreatedUtc = request.CreatedUtc
            };

            return await _repository.AddAsync(pet);
        }
    }
}
