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
    public class GetPetQueryTest
    {
        private readonly IMediator _mediator;

        public GetPetQueryTest()
        {
            var services = new ServiceCollection();
            services.AddMediatR(typeof(GetPetQuery));
            services.AddScoped<IPetRepository, MockPetRepository>();
            _mediator = services.BuildServiceProvider().GetService<IMediator>();
        }

        [Fact]
        public async void ShouldReturnPet()
        {
            // Arrange
            var expected = new Pet
            {
                Id = "1",
                Name = "Larry"
            };
            var query = new GetPetQuery
            {
                Id = expected.Id
            };

            // Act
            var result = await _mediator.Send(query);

            // Assert
            Assert.Equal(expected.Id, result.Id);
            Assert.Equal(expected.Name, result.Name);
        }
    }
}
