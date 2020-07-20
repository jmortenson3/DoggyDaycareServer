using Core.Common;
using Core.Pets;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Bookings
{
    public class BookingDetails : BaseEntity
    {
        public DateTime DropOffDateTime { get; set; }
        public DateTime PickUpDateTime { get; set; }
        public int PetId { get; set; }
        public Pet Pet { get; set; }
        public GroomingDetails GroomingDetails { get; set; }
        public BoardingDetails BoardingDetails { get; set; }
    }
}
