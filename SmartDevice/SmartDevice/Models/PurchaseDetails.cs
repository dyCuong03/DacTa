namespace SmartDevice.Models;

public class PurchaseDetails
{
    public required string PurchaseInvoiceId { get; set; }
    public required string ProductId { get; set; }
    public decimal Amount { get; set; }
    public required string SupplierId { get; set; }
    public Product? Product { get; set; }
    public Supplier? Supplier { get; set; }
    public PurchaseInvoice? PurchaseInvoice { get; set; }
}