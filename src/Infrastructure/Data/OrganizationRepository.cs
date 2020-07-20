using Core.Organizations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class OrganizationRepository : IOrganizationRepository
    {
        private readonly ApplicationContext _context;

        public OrganizationRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Organization> Add(Organization organization)
        {
            var entity = _context.Organizations.Add(organization);
            await _context.SaveChangesAsync();
            return entity.Entity;
        }

        public async Task<List<Organization>> Find(Func<Organization, bool> filter = null)
        {
            return await _context.Organizations.Where(filter).AsQueryable().ToListAsync();
        }

        public async Task<Organization> FindById(int id)
        {
            return await _context.Organizations.FindAsync(id);
        }

        public async Task<Organization> Update(Organization organization)
        {
            var entity = await _context.Organizations.FindAsync(organization.Id);
            entity.Name = organization.Name;
            entity.LastModifiedUtc = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
