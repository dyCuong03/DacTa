namespace SmartDevice.Models;

public class SalesDetails
{
    public required int SalesInvoiceId { get; set; }
    public required int ProductId { get; set; }
    public decimal Amount { get; set; }
    public Product? Product { get; set; }
    public SalesInvoice? SalesInvoice { get; set; }
}