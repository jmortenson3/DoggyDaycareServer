using DoggyDaycare.Core.Customers.Queries;
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
            await _mediator.Send(query);

            // Assert

        }
    }
}
