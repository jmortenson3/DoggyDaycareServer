using Core.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Bookings
{
    public class BookingDetails : BaseEntity
    {
        public int BookingId { get; set; }
        //public BookingDetailType BookingDetailType { get; set; }
        public string BookingDetailType { get; set; }
        public DateTime DropOffDateTime { get; set; }
        public DateTime PickUpDateTime { get; set; }
        public string Notes { get; set; }
        public string KennelType { get; set; }
        public bool DoNailClipping { get; set; }
        public bool DoHaircut { get; set; }
        public string HaircutStyle { get; set; }
    }
}
