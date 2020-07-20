using Core.Locations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<Location> Add(Location location)
        {
            var organization = await _context.Organizations.FindAsync(location.OrganizationId);
            if (organization.Locations == null)
            {
                organization.Locations = new List<Location>();
            }
            organization.Locations.Add(location);
            await _context.SaveChangesAsync();
            return location;
        }

        public async Task<List<Location>> Find(Func<Location, bool> filter = null)
        {
            return await _context.Locations.Where(filter).AsQueryable().ToListAsync();
        }

        public async Task<Location> FindById(int id)
        {
            return await _context.Locations.FindAsync(id);
        }

        public async Task<Location> Update(Location location)
        {
            var entity = await _context.Locations.FindAsync(location.Id);
            entity.Name = location.Name;
            entity.LastModifiedUtc = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
