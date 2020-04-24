using DoggyDaycare.Core.Organizations;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoggyDaycare.Core.Common
{
    public interface IOrganizationRepository : IAsyncRepository<Organization>
    {
    }
}
