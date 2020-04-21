using DoggyDaycare.Core.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoggyDaycare.Core.Customers.Entities
{
    public class Customer : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
