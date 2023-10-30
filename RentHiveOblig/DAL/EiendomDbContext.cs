using Microsoft.EntityFrameworkCore;
using RentHiveOblig.Models;

namespace RentHiveOblig.DAL
{
    public class EiendomnDbContext : DbContext
    {
        public EiendomnDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Eiendom> Eiendom { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<WishlistEiendom> WishlistEiendom { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Additional configuration for your database model can be added here.
            // For example, defining relationships, indexes, or constraints.
            // You can also use Fluent API to configure your model further.
        }
    }
}