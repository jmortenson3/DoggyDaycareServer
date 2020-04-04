using DoggyDaycare.Core.Common;
using DoggyDaycare.Core.Customers.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoggyDaycare.Infrastructure
{
    public class MockCustomerRepository : ICustomerRepository
    {
        private List<Customer> customers;
        public MockCustomerRepository()
        {
            customers = new List<Customer>();
            // Default customer 1
            var defaultCustomer = new Customer
            {
                Id = "1",
                Email = "test@test.com",
                Name = "Josiah"
            };
            customers.Add(defaultCustomer);
        }

        public Customer Find(string id)
        {
            var customer = customers.Find(customer => customer.Id == id);
            return customer;
        }
    }
}
