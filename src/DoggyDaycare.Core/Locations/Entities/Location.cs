using DoggyDaycare.Core.Bookings.Entities;
using DoggyDaycare.Core.Common;
using DoggyDaycare.Core.Organizations.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoggyDaycare.Core.Locations.Entities
{
    public class Location : IEntity
    {
        public Location()
        {
            KennelBookings = new HashSet<KennelBooking>();
        }

        public string Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedUtc { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedUtc { get; set; }
        public int OrganizationId { get; set; }
        public string Name { get; set; }

        public Organization Organization { get; set; }
        public ICollection<KennelBooking> KennelBookings { get; private set; }
    }
}
