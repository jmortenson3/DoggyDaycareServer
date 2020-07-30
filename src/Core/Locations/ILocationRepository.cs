using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Locations
{
    public interface ILocationRepository
    {
        void Add(Location location);
        Task<List<Location>> FindAllAsync(Expression<Func<Location, bool>> filter);
        Task<Location> FindAsync(int id);
        Task SaveAsync();
    }
}
