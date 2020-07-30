using Core.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Pets
{
    public class UpdatePetCommand : IRequest<Pet>
    {
        public Pet Pet { get; set; }
    }

    public class UpdatePetCommandHandler : IRequestHandler<UpdatePetCommand, Pet>
    {
        private readonly IPetRepository _petRepository;

        public UpdatePetCommandHandler(IPetRepository petRepository)
        {
            _petRepository = petRepository;
        }

        public async Task<Pet> Handle(UpdatePetCommand request, CancellationToken cancellationToken)
        {
            return await _petRepository.UpdateAsync(request.Pet);
        }
    }
}
