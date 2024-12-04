namespace SmartDevice.Models.Interfaces;

public interface IPurchaseInvoice
{
    public string PurchaseInvoiceId { get; set; }
    public DateTime InvoiceDate { get; set; }
    public decimal TotalAmount { get; set; }
    public string EmployeeId { get; set; }
}