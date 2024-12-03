using System.Drawing;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmartDevice.Models;
using SmartDevice.Models.Installer;

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

        builder.DatabaseBind();
    }
}