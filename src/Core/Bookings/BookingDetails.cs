using Core.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Bookings
{
    public abstract class BookingDetails : BaseEntity
    {
        public int BookingId { get; set; }
        public DateTime DropOffDateTime { get; set; }
        public DateTime PickUpDateTime { get; set; }
        public string Notes { get; set; }
    }
}
