using DoggyDaycare.Core.Customers.Entities;
using DoggyDaycare.Core.Customers.Commands;
using System;
using Xunit;
using DoggyDaycare.Core.Common;
using Moq;
using System.Threading;

namespace DoggyDaycare.Core.Tests.Customers.Commands
{
    public class CreateCustomerCommandTest
    {
        private readonly Mock<ICustomerRepository> _repository;

        public CreateCustomerCommandTest()
        {
            _repository = new Mock<ICustomerRepository>();
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
            var handler = new CreateCustomerCommandHandler(_repository.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);

        }
    }
}
