using Core.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Core.Bookings
{
    public class GroomingDetails : BaseEntity
    {
        public int BookingDetailsId { get; set; }
        [JsonIgnore]
        public BookingDetails BookingDetails { get; set; }
        public string CustomerNotes { get; set; }
    }
}
