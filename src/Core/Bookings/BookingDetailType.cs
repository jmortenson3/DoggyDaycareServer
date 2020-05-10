using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Bookings
{
    public class BookingDetailType
    {
        private BookingDetailType(string value) { Value = value; }
        public string Value { get; set; }

        public static BookingDetailType Boarding { get { return new BookingDetailType("Boarding"); } }
        public static BookingDetailType Grooming { get { return new BookingDetailType("Grooming"); } }
    }
}
