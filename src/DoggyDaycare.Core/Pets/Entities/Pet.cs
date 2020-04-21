using DoggyDaycare.Core.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoggyDaycare.Core.Pets.Entities
{
    public class Pet : BaseEntity
    {
        public string CustomerId { get; set; }
        public string Name { get; set; }
    }
}
