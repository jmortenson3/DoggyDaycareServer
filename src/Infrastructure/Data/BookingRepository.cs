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

        public void Add(Booking booking)
        {
            _context.Bookings.AddAsync(booking);
        }

        public async Task<List<Booking>> FindAsync(Func<Booking, bool> filter = null)
        {
            return await _context.Bookings.Where(filter).AsQueryable().ToListAsync();
        }

        public async Task<Booking> FindByIdAsync(int id)
        {
            return await _context.Bookings
                .Include(booking => booking.BookingDetails)
                .ThenInclude(bookingDetails => bookingDetails.GroomingDetails)
                .Include(booking => booking.BookingDetails)
                .ThenInclude(bookingDetails => bookingDetails.BoardingDetails)
                .FirstOrDefaultAsync(booking => booking.Id == id);
        }

        public async Task<List<Booking>> FindByOrganizationAsync(int organizationId)
        {
            var organization = await _context.Organizations.FindAsync(organizationId);
            var bookings = await _context.Bookings
                .Include(booking => booking.BookingDetails)
                .ThenInclude(bookingDetails => bookingDetails.BoardingDetails)
                .Include(booking => booking.BookingDetails)
                .ThenInclude(bookingDetails => bookingDetails.GroomingDetails)
                .Where(booking => booking.OrganizationId == organization.Id)
                .ToListAsync();
            return bookings;
        }

        public async Task<List<Booking>> FindByLocationAsync(int locationId)
        {
            var location = await _context.Locations.FindAsync(locationId);
            var bookings = await _context.Bookings
                .Include(booking => booking.BookingDetails)
                .ThenInclude(bookingDetails => bookingDetails.BoardingDetails)
                .Include(booking => booking.BookingDetails)
                .ThenInclude(bookingDetails => bookingDetails.GroomingDetails)
                .Where(booking => booking.LocationId == location.Id)
                .ToListAsync();
            return bookings;
        }

        public async Task<Booking> UpdateAsync(Booking booking)
        {
            var entity = await _context.Bookings.FindAsync(booking.Id);
            entity.ModifiedUtc = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
