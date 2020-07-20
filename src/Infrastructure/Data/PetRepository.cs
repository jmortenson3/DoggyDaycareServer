using Core.Pets;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class PetRepository : IPetRepository
    {
        private readonly ApplicationContext _context;

        public PetRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Pet> Add(Pet pet)
        {
            var entity = _context.Pets.Add(pet);
            await _context.SaveChangesAsync();
            return entity.Entity;
        }

        public async Task<List<Pet>> Find(Func<Pet, bool> filter = null)
        {
            return await _context.Pets.Where(filter).AsQueryable().ToListAsync();
        }

        public async Task<Pet> FindById(int id)
        {
            return await _context.Pets.FindAsync(id);
        }

        public async Task<Pet> Update(Pet pet)
        {
            var entity = await _context.Pets.FindAsync(pet.Id);
            entity.Name = pet.Name;
            entity.LastModifiedUtc = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
