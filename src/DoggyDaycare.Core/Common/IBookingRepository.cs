using DoggyDaycare.Core.Bookings.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoggyDaycare.Core.Common
{
    public interface IBookingRepository : IAsyncRepository<Booking>
    {
    }
}
