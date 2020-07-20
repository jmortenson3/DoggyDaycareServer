using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Pets
{
    public interface IPetRepository
    {
        Task<Pet> Add(Pet pet);
        Task<List<Pet>> Find(Func<Pet, bool> filter);
        Task<Pet> FindById(int id);
        Task<Pet> Update(Pet pet);
    }
}
