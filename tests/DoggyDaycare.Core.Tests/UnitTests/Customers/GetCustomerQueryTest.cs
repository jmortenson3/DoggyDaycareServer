﻿using DoggyDaycare.Core.Common;
using DoggyDaycare.Core.Customers;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xunit;

namespace DoggyDaycare.Core.Tests.Customers
{
    public class GetCustomerQueryTest
    {
        private readonly Mock<IAsyncRepository<Customer>> _repository;

        public GetCustomerQueryTest()
        {
            _repository = new Mock<IAsyncRepository<Customer>>();
            _repository.Setup(x => x.FindAsync(It.Is<string>(val => val == "1")))
                .ReturnsAsync(new Customer { Id = "1", Name = "Josiah", Email = "test@test.com"} );
        }

        [Fact]
        public async void ShouldReturnCustomer()
        {
            // Arrange
            var expected = new Customer
            {
                Id = "1",
                Name = "Josiah",
                Email = "test@test.com"
            };
            var query = new GetCustomerQuery
            {
                Id = expected.Id
            };

            // Act
            var handler = new GetCustomerQueryHandler(_repository.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expected.Id, result.Id);
            Assert.Equal(expected.Name, result.Name);
            Assert.Equal(expected.Email, result.Email);

        }

        [Fact]
        public async void ShouldReturnNullCustomer()
        {
            // Arrange
            var query = new GetCustomerQuery
            {
                Id = "-1"
            };

            // Act
            var handler = new GetCustomerQueryHandler(_repository.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Null(result);
        }
    }
}
