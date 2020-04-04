using DoggyDaycare.Core.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoggyDaycare.Core.Bookings.Entities
{
    public class GroomingBooking : Booking
    {
        public GroomingBookingDetails GroomingBookingDetails { get; set; }
    }
}
