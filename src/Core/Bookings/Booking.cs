using Core.Common;
using Core.Locations;
using Core.Organizations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Core.Bookings
{
    public class Booking : BaseEntity
    {
        public int LocationId { get; set; }
        [JsonIgnore]
        public Location Location { get; set; }
        public int OrganizationId { get; set; }
        [JsonIgnore]
        public Organization Organization{ get; set; }
        public string OwnerId { get; set; }
        public List<BookingDetails> BookingDetails { get; set; } = new List<BookingDetails>();
    }
}
