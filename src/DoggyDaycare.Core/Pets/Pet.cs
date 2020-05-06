using DoggyDaycare.Core.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoggyDaycare.Core.Pets
{
    public class Pet : BaseEntity
    {
        public string OwnerId { get; set; }
        public string Name { get; set; }
    }
}
