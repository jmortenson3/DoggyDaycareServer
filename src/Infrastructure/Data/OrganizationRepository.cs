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

        public async Task<List<Organization>> FindAll(string userId)
        {
            var organizations = _context.Organizations
                .Join(
                    _context.Memberships,
                    org => org.Id,
                    membership => membership.OrganizationId, 
                    (org, membership) => new { org, membership })
                .Where(q => q.membership.UserId == userId)
                .Select(org => org.org);
            return await organizations.ToListAsync();
        }

        public async Task<Organization> Find(int id, string userId)
        {
            var organization = _context.Organizations
                .Join(
                    _context.Memberships,
                    org => org.Id,
                    membership => membership.OrganizationId,
                    (org, membership) => new { org, membership })
                .Where(q => q.membership.UserId == userId)
                .Select(org => org.org)
                .FirstOrDefault(org => org.Id == id);
            return organization;
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
