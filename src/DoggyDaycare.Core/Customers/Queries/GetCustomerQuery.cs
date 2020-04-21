using DoggyDaycare.Core.Common;
using DoggyDaycare.Core.Customers.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DoggyDaycare.Core.Customers.Queries
{
    public class GetCustomerQuery : IRequest<Customer>
    {
        public string Id { get; set; }
    }

    public class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, Customer>
    {
        private readonly ICustomerRepository _repository;

        public GetCustomerQueryHandler(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task<Customer> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
        {
            var customer = await _repository.FindAsync(request.Id);
            return customer;
        }
    }
}
