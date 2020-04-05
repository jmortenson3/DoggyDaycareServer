using DoggyDaycare.Core.Common;
using DoggyDaycare.Core.Customers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DoggyDaycare.Infrastructure
{
    public class MockCustomerRepository : ICustomerRepository
    {
        private List<Customer> customers;
        public MockCustomerRepository()
        {
            customers = new List<Customer>();

            var defaultCustomer = new Customer
            {
                Id = "1",
                Email = "test@test.com",
                Name = "Josiah"
            };
            customers.Add(defaultCustomer);
        }

        public string Add(Customer customer)
        {
            customers.Add(customer);
            return customer.Id;
        }

        public Customer Find(string id)
        {
            var customer = customers.Find(customer => customer.Id == id);
            return customer;
        }

        public void Update(Customer customerChanges)
        {
            var customer = customers.FirstOrDefault(cus => cus.Id == customerChanges.Id);
            if (customer != null)
            {
                customer.Email = customerChanges.Email;
                customer.Name = customerChanges.Name;
            }
        }
    }
}
