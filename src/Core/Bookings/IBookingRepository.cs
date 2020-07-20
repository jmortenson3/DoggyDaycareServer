using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bookings
{
    public interface IBookingRepository
    {
        Task<Booking> Add(Booking booking);
        Task<List<Booking>> Find(Func<Booking, bool> filter);
        Task<Booking> FindById(int id);
        Task<Booking> Update(Booking booking);
    }
}
