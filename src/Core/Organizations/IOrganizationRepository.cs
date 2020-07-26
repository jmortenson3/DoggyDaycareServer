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
        Task<List<Organization>> FindAllWhere(Expression<Func<Organization, bool>> filter);
        Task<List<Organization>> FindAll(string userId);
        Task<Organization> Find(int id, string userId);
        Task Save();
    }
}
