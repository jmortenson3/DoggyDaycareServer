using System;
using System.Collections.Generic;
using System.Text;
using DoggyDaycare.Core.Bookings.Entities;
using DoggyDaycare.Core.Customers.Entities;
using DoggyDaycare.Core.Locations.Entities;
using DoggyDaycare.Core.Organizations.Entities;
using DoggyDaycare.Core.Pets.Entities;
using Microsoft.EntityFrameworkCore;

namespace DoggyDaycare.Infrastructure
{
    public class DoggyDaycareContext : DbContext
    {
        public DoggyDaycareContext(DbContextOptions<DoggyDaycareContext> options) : base(options)
        {
        }

        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Location> Locations{ get; set; }
        public DbSet<Customer> Customers{ get; set; }
        public DbSet<Pet> Pets{ get; set; }
        public DbSet<Booking> Bookings{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }
}
