using DoggyDaycare.Core.Bookings.Entities;
using DoggyDaycare.Core.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoggyDaycare.Infrastructure
{
    public class MockBookingRepository : IBookingRepository
    {
        private readonly List<Booking> bookings;

        public MockBookingRepository()
        {
            bookings = new List<Booking>();
            var defaultBooking = new KennelBooking
            {
                Id = "1"
            };
            bookings.Add(defaultBooking);
        }

        public string Add(Booking entity)
        {
            bookings.Add(entity);
            return entity.Id;
        }

        public Booking Find(string id)
        {
            return bookings.Find(b => b.Id == id);
        }

        public Booking Update(Booking entity)
        {
            throw new NotImplementedException();
        }
    }
}
