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
    }
}
