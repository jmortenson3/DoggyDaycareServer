using Core.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Locations
{
    public class Location : BaseEntity
    {
        public int OrganizationId { get; set; }
        public string Name { get; set; }
    }
}
