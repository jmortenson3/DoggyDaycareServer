using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoggyDaycare.API.Locations
{
    public class CreateLocationModel
    {
        public int OrganizationId { get; set; }
        public string Name { get; set; }
    }
}
