using DoggyDaycare.Core.Common;
using DoggyDaycare.Core.Pets.Commands;
using DoggyDaycare.Core.Pets.Entities;
using DoggyDaycare.Infrastructure;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DoggyDaycare.Core.Tests.Pets.Commands
{
    public class CreatePetCommandTest
    {
        private readonly IMediator _mediator;
        private readonly IPetRepository _repository;

        public CreatePetCommandTest()
        {
            var services = new ServiceCollection();
            services.AddMediatR(typeof(CreatePetCommand));
            services.AddScoped<IPetRepository, MockPetRepository>();
            var servicesProvider = services.BuildServiceProvider();
            _mediator = servicesProvider.GetService<IMediator>();
            _repository = servicesProvider.GetService<IPetRepository>();
        }

        [Fact]
        public async void ShouldCreatePet()
        {
            // Arrange
            var pet = new Pet {
                Id = "2",
                Name = "Stevie",
                CustomerId = "1"
            };
            var command = new CreatePetCommand
            {
                Pet = pet
            };

            // Act
            var resultId = await _mediator.Send(command);

            // Assert
            var result = _repository.Find(pet.Id);
            Assert.NotNull(result);
            Assert.Equal(pet.Id, resultId);
            Assert.Equal(pet.Name, result.Name);
            Assert.Equal(pet.CustomerId, result.CustomerId);
        }
    }
}
