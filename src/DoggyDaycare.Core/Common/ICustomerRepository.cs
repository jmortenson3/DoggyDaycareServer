using DoggyDaycare.Core.Customers.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoggyDaycare.Core.Common
{
    public interface ICustomerRepository
    {
        public Customer Find(string id);
        public string Add(Customer customer);
        public void Update(Customer customer);
    }
}
