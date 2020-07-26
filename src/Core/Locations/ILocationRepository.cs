using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Locations
{
    public interface ILocationRepository
    {
        Task Add(Location location);
        Task<List<Location>> FindAll(Expression<Func<Location, bool>> filter);
        Task<Location> FindById(int id);
        Task Save();
    }
}
