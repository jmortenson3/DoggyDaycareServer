using Core.Common;
using Core.Locations;
using Core.Organizations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Bookings
{
    public class Booking : BaseEntity
    {
        public int LocationId { get; set; }
        public Location Location { get; set; }
        public int OrganizationId { get; set; }
        public Organization Organization{ get; set; }
        public string OwnerId { get; set; }
        public List<BookingDetails> BookingDetails { get; set; } = new List<BookingDetails>();
    }
}
