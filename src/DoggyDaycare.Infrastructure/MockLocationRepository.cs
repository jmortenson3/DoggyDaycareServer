using DoggyDaycare.Core.Common;
using DoggyDaycare.Core.Locations.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DoggyDaycare.Infrastructure
{
    public class MockLocationRepository : ILocationRepository
    {
        private List<Location> locations;

        public MockLocationRepository()
        {
            locations = new List<Location>();
            var defaultLocation = new Location
            {
                Id = "1",
                Name = "South Store"
            };
            locations.Add(defaultLocation);
        }

        public string Add(Location entity)
        {
            locations.Add(entity);
            return entity.Id;
        }

        public Location Find(string id)
        {
            return locations.Find(location => location.Id == id);
        }

        public Location Update(Location locationChanges)
        {
            var location = locations.FirstOrDefault(p => p.Id == locationChanges.Id);

            if (location != null)
            {
                location.Name = locationChanges.Name;
            }

            return location;
        }
    }
}
