using DoggyDaycare.Core.Common;
using DoggyDaycare.Core.Customers;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xunit;

namespace DoggyDaycare.Core.Tests.Customers
{
    public class UpdateCustomerCommandTest
    {
        private readonly Mock<ICustomerRepository> _repository;

        public UpdateCustomerCommandTest()
        {
            _repository = new Mock<ICustomerRepository>();
            _repository.Setup(x => x.UpdateAsync(It.IsAny<Customer>())).ReturnsAsync(new Customer { Id = "1", Email = "newtest@test.com" } );

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
                Customer = customer
            };

            // Act
            var handler = new UpdateCustomerCommandHandler(_repository.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
        }
    }

}
