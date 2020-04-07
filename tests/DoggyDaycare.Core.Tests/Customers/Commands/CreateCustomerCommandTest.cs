using DoggyDaycare.Core.Customers.Entities;
using DoggyDaycare.Core.Customers.Commands;
using System;
using Xunit;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using DoggyDaycare.Infrastructure;
using DoggyDaycare.Core.Common;

namespace DoggyDaycare.Core.Tests.Customers.Commands
{
    public class CreateCustomerCommandTest
    {
        private readonly IMediator _mediator;
        private readonly ICustomerRepository _repository;

        public CreateCustomerCommandTest()
        {
            var services = new ServiceCollection();
            services.AddMediatR(typeof(CreateCustomerCommand));
            services.AddSingleton<ICustomerRepository, MockCustomerRepository>();
            var serviceProvider = services.BuildServiceProvider();
            _mediator = serviceProvider.GetService<IMediator>();
            _repository = serviceProvider.GetService<ICustomerRepository>();
        }

        [Fact]
        public async void ShouldCreateCustomer()
        {
            // Arrange
            var customer = new Customer {
                Id = "2",
                Email = "test2@test.com",
                Name = "Josiah2"
            };

            var command = new CreateCustomerCommand
            {
                Customer = customer
            };

            // Act
            await _mediator.Send(command);
            var result = _repository.Find(command.Customer.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(command.Customer.Id, result.Id);
            Assert.Equal(command.Customer.Email, result.Email);
            Assert.Equal(command.Customer.Name, result.Name);

        }
    }
}
