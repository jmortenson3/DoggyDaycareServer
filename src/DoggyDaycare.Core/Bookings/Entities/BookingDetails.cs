using System;
using System.Collections.Generic;
using System.Text;

namespace DoggyDaycare.Core.Bookings.Entities
{
    public abstract class BookingDetails
    {
        public DateTime DropOffDateTime { get; set; }
        public DateTime PickUpDateTime { get; set; }
        public string Notes { get; set; }
    }
}
