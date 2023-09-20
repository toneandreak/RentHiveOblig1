using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RentHiveOblig.Models;

namespace RentHiveOblig.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<RentHiveOblig.Models.Eiendom>? Eiendom { get; set; }
        public DbSet<RentHiveOblig.Models.Bruker>? Bruker { get; set; }

    }
}