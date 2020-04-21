using DoggyDaycare.Core.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoggyDaycare.Core.Bookings.Entities
{
    public abstract class BookingDetails : BaseEntity
    {
        public string BookingId { get; set; }
        public DateTime DropOffDateTime { get; set; }
        public DateTime PickUpDateTime { get; set; }
        public string Notes { get; set; }
    }
}
