using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bookings
{
    public interface IBookingRepository
    {
        void Add(Booking booking);
        Task<List<Booking>> FindAsync(Func<Booking, bool> filter);
        Task<Booking> FindByIdAsync(int id);
        Task<Booking> UpdateAsync(Booking booking);
        Task<List<Booking>> FindByOrganizationAsync(int organizationId);
        Task<List<Booking>> FindByLocationAsync(int locationId);
        Task SaveAsync();
    }
}
