using DoggyDaycare.Core.Common;
using DoggyDaycare.Core.Customers.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DoggyDaycare.Core.Customers.Commands
{
    public class UpdateCustomerCommand : IRequest
    {
        public Customer customerChanges { get; set; }

    }

    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand>
    {
        private readonly ICustomerRepository _repository;

        public UpdateCustomerCommandHandler(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = request.customerChanges;
            _repository.Update(customer);
            return Unit.Value;

        }
    }
}
