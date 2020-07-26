using Core.Memberships;
using Core.Organizations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public void Add(Organization organization)
        {
            _context.Organizations.Add(organization);
        }

        public async Task<List<Organization>> FindAllWhere(Expression<Func<Organization, bool>> filter)
        {
            return await _context.Organizations.Where(filter).ToListAsync();
        }

        public async Task<List<Organization>> FindAll()
        {
            return await _context.Organizations.ToListAsync();
        }

        public async Task<Organization> Find(int id)
        {
            return await _context.Organizations.FindAsync(id);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
