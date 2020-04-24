using System;
using Xunit;
using DoggyDaycare.Core.Common;
using Moq;
using System.Threading;
using DoggyDaycare.Core.Customers;

namespace DoggyDaycare.Core.Tests.Customers
{
    public class CreateCustomerCommandTest
    {
        private readonly Mock<ICustomerRepository> _repository;

        public CreateCustomerCommandTest()
        {
            _repository = new Mock<ICustomerRepository>();
            _repository.Setup(x => x.AddAsync(It.IsAny<Customer>()))
                .ReturnsAsync(new Customer { Id = "2", Email = "test2@test.com", Name = "Josiah2" });
        }

        [Fact]
        public async void ShouldCreateCustomer()
        {
            // Arrange
            var customer = new Customer
            {
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
