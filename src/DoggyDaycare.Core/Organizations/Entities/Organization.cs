using DoggyDaycare.Core.Bookings.Entities;
using DoggyDaycare.Core.Common;
using DoggyDaycare.Core.Locations.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoggyDaycare.Core.Organizations.Entities
{
    public class Organization: IEntity
    {
        public Organization()
        {
            Locations = new HashSet<Location>();
            KennelBookings = new HashSet<KennelBooking>();
        }

        public string Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedUtc { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedUtc { get; set; }
        public int OwnerId { get; set; }
        public string Name { get; set; }

        public ICollection<Location> Locations { get; private set; }
        public ICollection<KennelBooking> KennelBookings { get; private set; }
    }
}
