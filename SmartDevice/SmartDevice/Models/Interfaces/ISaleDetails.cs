namespace SmartDevice.Models.Interfaces;

public interface ISaleDetails
{
    public string SalesInvoiceId { get; set; }
    public string ProductId { get; set; }
    public decimal Amount { get; set; }
}