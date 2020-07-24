using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Organizations
{
    public interface IOrganizationRepository
    {
        Task<Organization> Add(Organization organization);
        Task<List<Organization>> FindAll(Expression<Func<Organization, bool>> filter);
        Task<List<Organization>> FindAll();
        Task<Organization> FindById(int id);
        Task<Organization> Update(Organization organization);
    }
}
