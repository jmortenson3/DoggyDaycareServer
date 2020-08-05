using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core.Bookings;
using Core.Common;
using Core.Locations;
using Core.Memberships;
using Core.Organizations;
using Core.Pets;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Membership> Memberships { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<BookingDetails> BookingDetails { get; set; }
        public DbSet<BoardingDetails> BoardingDetails { get; set; }
        public DbSet<GroomingDetails> GroomingDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var AddedEntities = ChangeTracker.Entries()
               .Where(E => E.State == EntityState.Added && E.Entity is AuditableEntity)
               .ToList();

            AddedEntities.ForEach(E =>
            {
                E.Property("CreatedUtc").CurrentValue = DateTime.UtcNow;
            });

            var EditedEntities = ChangeTracker.Entries()
                .Where(E => E.State == EntityState.Modified && E.Entity is AuditableEntity)
                .ToList();

            EditedEntities.ForEach(E =>
            {
                E.Property("ModifiedUtc").CurrentValue = DateTime.UtcNow;
            });
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
