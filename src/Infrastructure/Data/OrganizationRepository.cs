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

        public async Task<List<Organization>> FindAllWhereAsync(Expression<Func<Organization, bool>> filter)
        {
            return await _context.Organizations.Where(filter).ToListAsync();
        }

        public async Task<List<Organization>> FindAllAsync(string userId)
        {
            var myMemberships = await _context.Memberships.Include(m => m.Organization).Where(m => m.UserId == userId).ToListAsync();
            var organizations = new List<Organization>();

            foreach (var membership in myMemberships)
            {
                organizations.Add(membership.Organization);
            }

            return organizations.Distinct().ToList();
        }

        public Organization Find(int id, string userId)
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

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Organization> FindAsync(int id)
        {
            return await _context.Organizations.FindAsync(id);
        }
    }
}
