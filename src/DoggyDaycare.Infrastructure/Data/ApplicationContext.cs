using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using DoggyDaycare.Core.Bookings;
using DoggyDaycare.Core.Customers;
using DoggyDaycare.Core.Locations;
using DoggyDaycare.Core.Organizations;
using DoggyDaycare.Core.Pets;
using Microsoft.EntityFrameworkCore;

namespace DoggyDaycare.Infrastructure.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }
    }
}
