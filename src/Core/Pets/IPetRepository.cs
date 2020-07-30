using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Pets
{
    public interface IPetRepository
    {
        void Add(Pet pet);
        Task<List<Pet>> FindByOwner(string ownerId);
        Task<Pet> FindByIdAsync(int id);
        Task<Pet> UpdateAsync(Pet pet);
        Task SaveAsync();
    }
}
