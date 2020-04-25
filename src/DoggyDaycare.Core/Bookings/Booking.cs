using DoggyDaycare.Core.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoggyDaycare.Core.Bookings
{
    public class Booking : BaseEntity
    {
        public string LocationId { get; set; }
        public string OrganizationId { get; set; }
        public string CustomerId { get; set; }
    }
}
