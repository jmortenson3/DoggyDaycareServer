using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Organizations
{
    public interface IOrganizationRepository
    {
        void Add(Organization organization);
        Task<List<Organization>> FindAllWhereAsync(Expression<Func<Organization, bool>> filter);
        Task<List<Organization>> FindAllAsync(string userId);
        Organization Find(int id, string userId);
        Task<Organization> FindAsync(int id);
        Task SaveAsync();
    }
}
