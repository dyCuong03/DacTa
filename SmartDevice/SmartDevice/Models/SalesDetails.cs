namespace SmartDevice.Models;

public class SalesDetails
{
    public required string SalesInvoiceId { get; set; }
    public required string ProductId { get; set; }
    public decimal Amount { get; set; }
    public Product? Product { get; set; }
    public SalesInvoice? SalesInvoice { get; set; }
}