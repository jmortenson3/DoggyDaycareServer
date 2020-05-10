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
        private readonly IAsyncRepository<Pet> _repository;

        public UpdatePetCommandHandler(IAsyncRepository<Pet> repository)
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
