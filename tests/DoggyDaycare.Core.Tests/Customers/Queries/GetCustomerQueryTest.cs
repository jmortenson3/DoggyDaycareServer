using DoggyDaycare.Core.Common;
using DoggyDaycare.Core.Customers.Queries;
using DoggyDaycare.Infrastructure;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DoggyDaycare.Core.Tests.Customers.Queries
{
    public class GetCustomerQueryTest
    {

        private readonly IMediator _mediator;

        public GetCustomerQueryTest()
        {
            var services = new ServiceCollection();
            services.AddMediatR(typeof(GetCustomerQuery));
            services.AddScoped<ICustomerRepository, MockCustomerRepository>();
            _mediator = services.BuildServiceProvider().GetService<IMediator>();
        }

        [Fact]
        public async void ShouldReturnCustomer()
        {
            // Arrange
            var query = new GetCustomerQuery
            {
                Id = "1"
            };

            // Act
            var customer = await _mediator.Send(query);

            // Assert
            Assert.Equal(query.Id, customer.Id);
            Assert.Equal("Josiah", customer.Name);
            Assert.Equal("test@test.com", customer.Email);

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
            var customer = await _mediator.Send(query);

            // Assert
            Assert.Null(customer);
        }
    }
}
