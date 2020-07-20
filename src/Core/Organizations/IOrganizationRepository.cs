using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Organizations
{
    public interface IOrganizationRepository
    {
        Task<Organization> Add(Organization organization);
        Task<List<Organization>> Find(Func<Organization, bool> filter);
        Task<Organization> FindById(int id);
        Task<Organization> Update(Organization organization);
    }
}
