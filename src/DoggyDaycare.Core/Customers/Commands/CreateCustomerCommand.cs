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
    public class CreateCustomerCommand : IRequest<string>
    {
        public Customer Customer { get; set; }
    }

    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, string>
    {
        private readonly ICustomerRepository _repository;
        public CreateCustomerCommandHandler(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task<string> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var entity = new Customer
            {
                Id = request.Customer.Id,
                Email = request.Customer.Email,
                Name = request.Customer.Name
            };

            var id = _repository.Add(entity);
            return id;
        }
    }
} 
