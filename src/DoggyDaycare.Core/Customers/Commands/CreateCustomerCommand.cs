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
        public string Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
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
            return request.Id;
        }
    }
} 
