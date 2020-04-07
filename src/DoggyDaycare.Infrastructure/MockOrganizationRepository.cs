using DoggyDaycare.Core.Common;
using DoggyDaycare.Core.Organizations.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DoggyDaycare.Infrastructure
{
    public class MockOrganizationRepository : IOrganizationRepository
    {
        private List<Organization> organizations;

        public MockOrganizationRepository()
        {
            organizations = new List<Organization>();
            var defaultOrganization = new Organization
            {
                Id = "1",
                Name = "DoggyDaycare"
            };
            organizations.Add(defaultOrganization);
        }

        public string Add(Organization entity)
        {
            organizations.Add(entity);
            return entity.Id;
        }

        public Organization Find(string id)
        {
            return organizations.FirstOrDefault(org => org.Id == id);
        }

        public Organization Update(Organization organizationChanges)
        {
            var organization = organizations.FirstOrDefault(cus => cus.Id == organizationChanges.Id);

            if (organization != null)
            {
                organization.Name = organizationChanges.Name;
            }

            return organization;
        }
    }
}
