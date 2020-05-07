using DoggyDaycare.Core.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoggyDaycare.Core.Organizations
{
    public class Organization : BaseEntity
    {
        public string OwnerId { get; set; }
        public string Name { get; set; }
    }
}
