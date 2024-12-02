using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmartDevice.Models;

namespace SmartDevice.Datas;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }
    
    public DbSet<Product> Products { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<ProductDetails> ProductDetails { get; set; }
    public DbSet<PurchaseInvoice> PurchaseInvoices { get; set; }
    public DbSet<PurchaseDetails> PurchaseDetails { get; set; }
    public DbSet<SalesDetails> SalesDetails { get; set; }
    public DbSet<SalesInvoice> SalesInvoices { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Ignore<IdentityRole>();
        builder.Ignore<IdentityUserRole<string>>();
        builder.Ignore<IdentityUserClaim<string>>();
        builder.Ignore<IdentityUserLogin<string>>();
        builder.Ignore<IdentityRoleClaim<string>>();
        builder.Ignore<IdentityUserToken<string>>();
        
        builder.Entity<PurchaseDetails>()
            .HasKey(pd => new { pd.ProductId, pd.PurchaseInvoiceId });

        builder.Entity<PurchaseDetails>()
            .HasOne(pd => pd.PurchaseInvoice) 
            .WithMany(pi => pi.PurchaseDetails)
            .HasForeignKey(pd => pd.PurchaseInvoiceId);
        
                
        builder.Entity<SalesDetails>()
            .HasKey(pd => new { pd.ProductId, pd.SalesInvoiceId });

        builder.Entity<SalesDetails>()
            .HasOne(pd => pd.SalesInvoice) 
            .WithMany(pi => pi.SalesDetails)
            .HasForeignKey(pd => pd.SalesInvoiceId);

        builder.Entity<ProductDetails>()
            .HasKey(pd => new { pd.ProductId });

        builder.Entity<ProductDetails>()
            .HasOne(pd => pd.Product)
            .WithMany(p => p.ProductDetails) 
            .HasForeignKey(pd => pd.ProductId)
            .OnDelete(DeleteBehavior.Cascade); 
        
    }
}