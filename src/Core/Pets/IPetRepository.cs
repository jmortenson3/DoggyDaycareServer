using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Pets
{
    public interface IPetRepository
    {
        void Add(Pet pet);
        Task<List<Pet>> FindAsync(Func<Pet, bool> filter);
        Task<Pet> FindByIdAsync(int id);
        Task<Pet> UpdateAsync(Pet pet);
        Task SaveAsync();
    }
}
