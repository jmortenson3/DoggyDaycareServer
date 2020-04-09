using DoggyDaycare.Core.Common;
using DoggyDaycare.Core.Pets.Entities;
using DoggyDaycare.Core.Pets.Queries;
using DoggyDaycare.Infrastructure;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DoggyDaycare.Core.Tests.Pets.Queries
{
    public class GetPetsByCustomerQueryTest
    {
        private readonly IMediator _mediator;

        public GetPetsByCustomerQueryTest()
        {
            var services = new ServiceCollection();
            services.AddMediatR(typeof(GetPetsByCustomerQuery));
            services.AddScoped<IPetRepository, MockPetRepository>();
            _mediator = services.BuildServiceProvider().GetService<IMediator>();
        }

        [Fact]
        public async void ShouldReturnPetsForDefaultCustomer()
        {
            // Arrange
            var expected = new Pet
            {
                Id = "1",
                Name = "Larry",
                CustomerId = "1"
            };
            var query = new GetPetsByCustomerQuery
            {
                CustomerId = expected.CustomerId
            };

            // Act
            var result = await _mediator.Send(query);

            // Assert
            Assert.True(result.Count > 0);
            var resultPet = result.Find(pet => pet.Id == expected.Id);
            Assert.NotNull(resultPet);
            Assert.Equal(expected.Id, resultPet.Id);
            Assert.Equal(expected.Name, resultPet.Name);
            Assert.Equal(expected.CustomerId, resultPet.CustomerId);
        }
    }
}
