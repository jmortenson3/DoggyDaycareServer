using DoggyDaycare.Core.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoggyDaycare.Core.Organizations.Entities
{
    public class Organization: BaseEntity
    {
        public int OwnerId { get; set; }
        public string Name { get; set; }
    }
}
