using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoggyDaycare.Infrastructure.Identity
{
    public class IdentityContext : IdentityDbContext<ApplicationUser>
    {
        public IdentityContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>(b =>
            {
                b.HasKey(u => u.Id);

                b.HasIndex(u => u.NormalizedEmail).HasName("EmailIndex").IsUnique();

                b.ToTable("DoggyDaycareUsers");
            });
        }
    }
}
