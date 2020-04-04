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
            var command = new CreateCustomerCommand
            {
                Id = "2",
                Email = "test2@test.com",
                Name = "Josiah2"

            };

            // Act
            await _mediator.Send(command);
            var customer = _repository.Find(command.Id);

            // Assert
            Assert.NotNull(customer);
            Assert.Equal(command.Id, customer.Id);
            Assert.Equal(command.Email, customer.Email);
            Assert.Equal(command.Name, customer.Name);

        }
    }
}
