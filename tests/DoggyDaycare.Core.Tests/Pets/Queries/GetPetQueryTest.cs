using DoggyDaycare.Core.Common;
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
            var query = new GetPetQuery
            {
                Id = "1"
            };

            // Act
            var entity = await _mediator.Send(query);

            // Assert
            Assert.Equal(query.Id, entity.Id);
            Assert.Equal("Larry", entity.Name);
        }
    }
}
