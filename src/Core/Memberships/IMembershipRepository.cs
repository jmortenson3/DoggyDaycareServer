using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Memberships
{
    public interface IMembershipRepository
    {
        void Add(Membership membership);
        Task<List<Membership>> FindAllWhereAsync(Expression<Func<Membership, bool>> filter);
        Task<List<Membership>> FindAllAsync();
        Task<Membership> FindByIdAsync(int id);
        Task<Membership> UpdateAsync(Membership membership);
    }
}
