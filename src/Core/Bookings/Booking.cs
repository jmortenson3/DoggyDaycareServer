using Core.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Bookings
{
    public class Booking : BaseEntity
    {
        public int LocationId { get; set; }
        public int OrganizationId { get; set; }
        public string OwnerId { get; set; }
        public List<BookingDetails> BookingDetails { get; set; }
    }
}
