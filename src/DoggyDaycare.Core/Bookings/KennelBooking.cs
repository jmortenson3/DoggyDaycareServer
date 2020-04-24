using System;
using System.Collections.Generic;
using System.Text;

namespace DoggyDaycare.Core.Bookings
{
    public class KennelBooking : Booking
    {
        public KennelBookingDetails KennelBookingDetails { get; set; }
        public GroomingBookingDetails GroomingBookingDetails { get; set; }
    }
}
