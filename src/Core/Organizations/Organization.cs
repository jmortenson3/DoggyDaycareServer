using Core.Bookings;
using Core.Common;
using Core.Locations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Core.Organizations
{
    public class Organization : BaseEntity
    {
        public string OwnerId { get; set; }
        public string Name { get; set; }
        public List<Location> Locations { get; set; }
        public List<Booking> Bookings { get; set; }
    }
}
