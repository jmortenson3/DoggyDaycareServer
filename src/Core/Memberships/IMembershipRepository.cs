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
        Task<List<Membership>> FindAllWhere(Expression<Func<Membership, bool>> filter);
        Task<List<Membership>> FindAll();
        Task<Membership> FindById(int id);
        Task<Membership> Update(Membership membership);
    }
}
