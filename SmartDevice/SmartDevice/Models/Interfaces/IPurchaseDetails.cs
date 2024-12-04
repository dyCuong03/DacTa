namespace SmartDevice.Models.Interfaces;

public interface IPurchaseDetails
{
    public string PurchaseInvoiceId { get; set; }
    public string ProductId { get; set; }
    public decimal Amount { get; set; }
    public string SupplierId { get; set; }
}