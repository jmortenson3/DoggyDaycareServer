using DoggyDaycare.Core.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoggyDaycare.Core.Bookings
{
    public class Booking : BaseEntity
    {
        public int LocationId { get; set; }
        public int OrganizationId { get; set; }
        public string OwnerId { get; set; }
    }
}
