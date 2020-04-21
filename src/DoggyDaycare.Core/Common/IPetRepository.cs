using DoggyDaycare.Core.Pets.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DoggyDaycare.Core.Common
{
    public interface IPetRepository : IAsyncRepository<Pet>
    {
        public Task<List<Pet>> FindAllAsync(Predicate<Pet> match);
    }
}
