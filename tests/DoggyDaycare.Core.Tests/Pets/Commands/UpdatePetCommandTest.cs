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
    public class UpdatePetCommandTest
    {
        private readonly IMediator _mediator;
        private readonly IPetRepository _repository;
        public UpdatePetCommandTest()
        {
            var services = new ServiceCollection();
            services.AddMediatR(typeof(UpdatePetCommand));
            services.AddScoped<IPetRepository, MockPetRepository>();
            var servicesProvider = services.BuildServiceProvider();
            _mediator = servicesProvider.GetService<IMediator>();
            _repository = servicesProvider.GetService<IPetRepository>();
        }

        [Fact]
        public async void ShouldUpdatePet()
        {
            // Arrange
            var updatedPet = new Pet
            {
                Id = "1",
                Name = "Pickles"
            };
            var command = new UpdatePetCommand
            {
                Pet = updatedPet
            };

            // Act
            var pet = await _mediator.Send(command);

            // Assert
            var result = _repository.Find(updatedPet.Id);
            Assert.NotNull(pet);
            Assert.Equal(updatedPet.Name, result.Name);
        }
    }
}
