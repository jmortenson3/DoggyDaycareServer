using Core.Locations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class LocationRepository : ILocationRepository
    {
        private readonly ApplicationContext _context;

        public LocationRepository(ApplicationContext context)
        {
            _context = context;
        }

        public void Add(Location location)
        {
            _context.Locations.Add(location);
        }

        public async Task<List<Location>> FindAllAsync(Expression<Func<Location, bool>> filter = null)
        {
            return await _context.Locations.Where(filter).ToListAsync();
        }

        public async Task<Location> FindAsync(int id)
        {
            return await _context.Locations.FindAsync(id);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
