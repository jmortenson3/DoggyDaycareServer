using DoggyDaycare.Core.Bookings.Entities;
using DoggyDaycare.Core.Common;
using DoggyDaycare.Core.Pets.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoggyDaycare.Core.Customers.Entities
{
    public class Customer : IEntity
    {
        public Customer()
        {
            Pets = new HashSet<Pet>();
            KennelBookings = new HashSet<KennelBooking>();
        }

        public string Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedUtc { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedUtc { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public ICollection<Pet> Pets { get; private set; }
        public ICollection<KennelBooking> KennelBookings { get; private set; }
    }
}
