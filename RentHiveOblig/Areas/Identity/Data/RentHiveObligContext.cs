using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace RentHiveOblig.Data;

public class RentHiveObligContext : IdentityDbContext<IdentityUser>
{
    public RentHiveObligContext(DbContextOptions<RentHiveObligContext> options)
        : base(options)
    {

    }
    public DbSet<RentHiveOblig.Models.Eiendom> Eiendom { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
