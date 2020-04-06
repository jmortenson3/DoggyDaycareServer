using DoggyDaycare.Core.Pets.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoggyDaycare.Core.Common
{
    public interface IPetRepository
    {
        public Pet Find(string id);
    }
}
