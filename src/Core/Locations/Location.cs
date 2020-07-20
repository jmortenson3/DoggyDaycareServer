using Core.Bookings;
using Core.Common;
using Core.Organizations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Core.Locations
{
    public class Location : BaseEntity
    {
        public string Name { get; set; }
        public int OrganizationId { get; set; }
        [JsonIgnore]
        public Organization Organization { get; set; }
        public List<Booking> Bookings { get; set; }
    }
}
