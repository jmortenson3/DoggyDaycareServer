using DoggyDaycare.Core.Common;
using DoggyDaycare.Core.Customers.Entities;
using DoggyDaycare.Core.Pets.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoggyDaycare.Core.Bookings.Entities
{
    public abstract class Booking : IEntity
    {
        public Booking()
        {
            Pets = new HashSet<Pet>();
        }

        public string Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedUtc { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedUtc { get; set; }
        public int LocationId { get; set; }
        public int OrganizationId { get; set; }

        public Customer Customer { get; set; }
        public ICollection<Pet> Pets { get; set; }
    }
}
