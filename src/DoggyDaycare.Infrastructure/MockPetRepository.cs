using DoggyDaycare.Core.Common;
using DoggyDaycare.Core.Pets.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoggyDaycare.Infrastructure
{
    public class MockPetRepository : IPetRepository
    {
        private List<Pet> pets;
        public MockPetRepository()
        {
            var defaultPet = new Pet
            {
                Id = "1",
                Name = "Larry"
            };

            pets = new List<Pet>();
            pets.Add(defaultPet);
        }

        public string Add(Pet pet)
        {
            pets.Add(pet);
            return pet.Id;
        }

        public Pet Find(string id)
        {
            return pets.Find(pet => pet.Id == id);
        }
    }
}
