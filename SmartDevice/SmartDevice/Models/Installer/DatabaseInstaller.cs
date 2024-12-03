using Microsoft.EntityFrameworkCore;

namespace SmartDevice.Models.Installer;

public static class DatabaseInstaller
{
    public static void DatabaseBind(this ModelBuilder builder)
    {
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