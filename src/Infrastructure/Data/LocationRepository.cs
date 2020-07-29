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

        public async Task Add(Location location)
        {
            var organization = await _context.Organizations.FindAsync(location.OrganizationId);
            if (organization == null)
            {
                return;
            }

            if (organization.Locations == null)
            {
                organization.Locations = new List<Location>();
            }

            _context.Locations.Add(location);
            organization.Locations.Add(location);
            location.Organization = organization;
        }

        public async Task<List<Location>> FindAll(Expression<Func<Location, bool>> filter = null)
        {
            return await _context.Locations.Where(filter).ToListAsync();
        }

        public async Task<Location> FindById(int id)
        {
            return await _context.Locations.FindAsync(id);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
