using Core.Memberships;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class MembershipRepository : IMembershipRepository
    {
        private readonly ApplicationContext _context;

        public MembershipRepository(ApplicationContext context)
        {
            _context = context;
        }

        public void Add(Membership membership)
        {
            _context.Memberships.Add(membership);
        }

        public Task<List<Membership>> FindAllWhere(Expression<Func<Membership, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Membership>> FindAll()
        {
            return await _context.Memberships.ToListAsync();
        }

        public Task<Membership> FindById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Membership> Update(Membership membership)
        {
            throw new NotImplementedException();
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
