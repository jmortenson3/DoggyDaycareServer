using DoggyDaycare.Core.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DoggyDaycare.Core.Customers
{
    public class GetCustomerQuery : IRequest<Customer>
    {
        public string Id { get; set; }
    }

    public class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, Customer>
    {
        private readonly IAsyncRepository<Customer> _repository;

        public GetCustomerQueryHandler(IAsyncRepository<Customer> repository)
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
