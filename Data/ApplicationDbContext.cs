using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PayPlus.Models;

namespace PayPlus.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    
    public DbSet<Partner> Partners { get; set; }

public DbSet<PayPlus.Models.Service> Service { get; set; } = default!;
}

