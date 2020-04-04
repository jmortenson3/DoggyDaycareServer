using DoggyDaycare.Core.Common;
using DoggyDaycare.Core.Customers.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoggyDaycare.Core.Pets.Entities
{
    public class Pet : IEntity
    {
        public string Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedUtc { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedUtc { get; set; }
        public int CustomerId { get; set; }
        public string Name { get; set; }

        public Customer Customer { get; set; }
    }
}
