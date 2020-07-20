using Core.Bookings;
using Core.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Pets
{
    public class Pet : BaseEntity
    {
        public string OwnerId { get; set; }
        public string Name { get; set; }
        public List<BookingDetails> BookingDetails { get; set; }
    }
}
