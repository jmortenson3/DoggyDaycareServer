using DoggyDaycare.Core.Customers.Entities;
using DoggyDaycare.Core.Customers.Commands;
using System;
using Xunit;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DoggyDaycare.Core.Tests.Customers.Commands
{
    public class CreateCustomerCommandTest
    {
        private readonly IMediator _mediator;
        public CreateCustomerCommandTest()
        {
            var services = new ServiceCollection();
            services.AddMediatR(typeof(CreateCustomerCommand));
            _mediator = services.BuildServiceProvider().GetService<IMediator>();
        }

        [Fact]
        public async void ShouldCreateCustomer()
        {
            // Arrange
            var command = new CreateCustomerCommand
            {
                Id = "1",
                Email = "test@test.com",
                Name = "Josiah"

            };

            // Act
            var resultCustomerId = await _mediator.Send(command);

            // Assert
            Assert.Equal(command.Id, resultCustomerId);
        }
    }
}
