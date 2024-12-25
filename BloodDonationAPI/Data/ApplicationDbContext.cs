// File: BloodDonationAPI/Data/ApplicationDbContext.cs

using Microsoft.EntityFrameworkCore;
using BloodDonationAPI.Models;

namespace BloodDonationAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Event> Events { get; set; }

        // New DbSet for Event Registrations
        public DbSet<EventRegistration> EventRegistrations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Optional: Configure composite unique constraint to prevent duplicate registrations
            modelBuilder.Entity<EventRegistration>()
                .HasIndex(er => new { er.EventId, er.UserId })
                .IsUnique();
        }
    }
}
