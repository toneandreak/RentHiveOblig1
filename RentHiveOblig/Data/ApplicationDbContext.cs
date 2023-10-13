using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RentHiveOblig.Models;

namespace RentHiveOblig.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<RentHiveOblig.Models.Eiendom>? Eiendom { get; set; }
        public DbSet<RentHiveOblig.Models.Message>? Message { get; set; }
        public DbSet<RentHiveOblig.Models.Conversation>? Conversation { get; set; }
        public DbSet<RentHiveOblig.Models.Review>? Review { get; set; }
        public DbSet<RentHiveOblig.Models.Wishlist>? Wishlist { get; set; }
        public DbSet<RentHiveOblig.Models.WishlistEiendom>? WishlistEiendom { get; set; }
        public DbSet<RentHiveOblig.Models.Booking>? Booking { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }


    }
}