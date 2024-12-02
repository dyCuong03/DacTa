namespace SmartDevice.Models;

public class PurchaseDetails
{
    public required int PurchaseInvoiceId { get; set; }
    public required int ProductId { get; set; }
    public decimal Amount { get; set; }
    public required int SupplierId { get; set; }
    public Product? Product { get; set; }
    public Supplier? Supplier { get; set; }
    public PurchaseInvoice? PurchaseInvoice { get; set; }
}