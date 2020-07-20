using Core.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Bookings
{
    public class BoardingDetails : BaseEntity
    {
        public string KennelType { get; set; }
        public string CustomerNotes { get; set; }
    }
}
