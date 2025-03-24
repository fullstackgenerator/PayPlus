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
    public DbSet<Offer> Offers { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // many-to-many relationship between Offer and Service
        modelBuilder.Entity<Offer>()
            .HasMany(o => o.Services)
            .WithMany(s => s.Offers)
            .UsingEntity(j => j.ToTable("OfferServices"));

        // many-to-many relationship between Invoice and Service
        modelBuilder.Entity<Invoice>()
            .HasMany(i => i.Services)
            .WithMany(s => s.Invoices)
            .UsingEntity(j => j.ToTable("InvoiceServices"));
    }
    
public DbSet<PayPlus.Models.Service> Service { get; set; } = default!;

public DbSet<PayPlus.Models.Partner> Partner { get; set; } = default!;
public DbSet<PayPlus.Models.TravelOrder> TravelOrder { get; set; } = default!;

public DbSet<PayPlus.Models.Offer> Offer { get; set; } = default!;

public DbSet<PayPlus.Models.Invoice> Invoice { get; set; } = default!;
}

