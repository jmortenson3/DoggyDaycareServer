using DoggyDaycare.Core.Customers;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoggyDaycare.Core.Common
{
    public interface ICustomerRepository : IAsyncRepository<Customer>
    {
    }
}
