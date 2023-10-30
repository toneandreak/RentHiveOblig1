using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RentHiveOblig.Models;

namespace RentHiveOblig.DAL
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Eiendom>? Eiendom { get; set; }
        public DbSet<Message>? Message { get; set; }
        public DbSet<Conversation>? Conversation { get; set; }
        public DbSet<Review>? Review { get; set; }
        public DbSet<Wishlist>? Wishlist { get; set; }
        public DbSet<WishlistEiendom>? WishlistEiendom { get; set; }
        public DbSet<Booking>? Booking { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }


    }
}