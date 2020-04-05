using DoggyDaycare.Core.Common;
using DoggyDaycare.Core.Customers.Commands;
using DoggyDaycare.Core.Customers.Entities;
using DoggyDaycare.Infrastructure;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DoggyDaycare.Core.Tests.Customers.Commands
{
    public class UpdateCustomerCommandTest
    {
        private readonly IMediator _mediator;
        private readonly ICustomerRepository _repository;

        public UpdateCustomerCommandTest()
        {
            var services = new ServiceCollection();
            services.AddMediatR(typeof(UpdateCustomerCommand));
            services.AddScoped<ICustomerRepository, MockCustomerRepository>();
            var serviceProvider = services.BuildServiceProvider();
            _mediator = serviceProvider.GetService<IMediator>();
            _repository = serviceProvider.GetService<ICustomerRepository>();
            
        }

        [Fact]
        public async void ShouldUpdateCustomerEmail()
        {
            // Arrange
            var customer = new Customer
            {
                Id = "1",
                Email = "newtest@test.com"
            };

            var command = new UpdateCustomerCommand
            {
                customerChanges = customer
            };

            // Act
            await _mediator.Send(command);

            // Assert
            var updatedCustomer = _repository.Find(customer.Id);

            Assert.Equal(customer.Id, updatedCustomer.Id);
            Assert.Equal(customer.Email, updatedCustomer.Email);
        }
    }

}
