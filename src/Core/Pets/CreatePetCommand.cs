using Core.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Pets
{
    public class CreatePetCommand : IRequest<Pet>
    {
        [Required]
        public string OwnerId { get; set; }
        [Required]
        public string Name { get; set; }
        [JsonIgnore]
        public string CreatedBy { get; set; }
        [JsonIgnore]
        public DateTime CreatedUtc { get; set; }
    }

    public class CreatePetCommandHandler : IRequestHandler<CreatePetCommand, Pet>
    {
        private readonly IPetRepository _petRepository;

        public CreatePetCommandHandler(IPetRepository petRepository)
        {
            _petRepository = petRepository;
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

            _petRepository.Add(pet);
            await _petRepository.SaveAsync();
            return pet;
        }
    }
}
