using Core.Bookings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class BookingRepository : IBookingRepository
    {
        private readonly ApplicationContext _context;

        public BookingRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Booking> Add(Booking booking)
        {
            var entity = _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();
            return entity.Entity;

        }

        public async Task<List<Booking>> Find(Func<Booking, bool> filter = null)
        {
            return await _context.Bookings.Where(filter).AsQueryable().ToListAsync();
        }

        public async Task<Booking> FindById(int id)
        {
            return await _context.Bookings.FindAsync(id);
        }

        public async Task<Booking> Update(Booking booking)
        {
            var entity = await _context.Bookings.FindAsync(booking.Id);
            entity.LastModifiedUtc = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
