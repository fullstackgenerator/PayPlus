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
    public DbSet<Service> Services { get; set; }
    public DbSet<TravelOrder> TravelOrders { get; set; }
    

public DbSet<PayPlus.Models.Service> Service { get; set; } = default!;

public DbSet<PayPlus.Models.Partner> Partner { get; set; } = default!;
public DbSet<PayPlus.Models.TravelOrder> TravelOrder { get; set; } = default!;
}

