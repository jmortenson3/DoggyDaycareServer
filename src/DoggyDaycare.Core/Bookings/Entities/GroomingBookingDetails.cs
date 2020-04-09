using System;
using System.Collections.Generic;
using System.Text;

namespace DoggyDaycare.Core.Bookings.Entities
{
    public class GroomingBookingDetails : BookingDetails
    {
        public bool DoNailClipping { get; set; }
        public bool DoHaircut { get; set; }
        public string HaircutStyle { get; set; }
    }
}
