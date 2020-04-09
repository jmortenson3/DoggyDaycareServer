using System;
using System.Collections.Generic;
using System.Text;

namespace DoggyDaycare.Core.Bookings.Entities
{
    public abstract class BookingDetails
    {
        public string Id { get; set; }
        public string BookingId { get; set; }
        public DateTime DropOffDateTime { get; set; }
        public DateTime PickUpDateTime { get; set; }
        public string Notes { get; set; }
    }
}
