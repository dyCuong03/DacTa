using SmartDevice.Models;
using SmartDevice.Models.Interfaces;

namespace SmartDevice.DTOs;

public class PurchaseInvoiceDto : IPurchaseInvoice
{
    public string PurchaseInvoiceId { get; set; }
    public DateTime InvoiceDate { get; set; }
    public decimal TotalAmount { get; set; }
    public string? EmployeeId { get; set; }
}