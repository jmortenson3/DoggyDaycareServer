using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Locations
{
    public interface ILocationRepository
    {
        Task<Location> Add(Location location);
        Task<List<Location>> Find(Func<Location, bool> filter);
        Task<Location> FindById(int id);
        Task<Location> Update(Location location);
    }
}
