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

        public void Add(Pet pet)
        {
            _context.Pets.Add(pet);
        }

        public async Task<List<Pet>> FindAsync(Func<Pet, bool> filter = null)
        {
            return await _context.Pets.Where(filter).AsQueryable().ToListAsync();
        }

        public async Task<Pet> FindByIdAsync(int id)
        {
            return await _context.Pets.FindAsync(id);
        }

        public async Task<Pet> UpdateAsync(Pet pet)
        {
            var entity = await _context.Pets.FindAsync(pet.Id);
            entity.Name = pet.Name;
            entity.LastModifiedUtc = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
