using SmartDevice.Models;
using SmartDevice.Models.Interfaces;

namespace SmartDevice.DTOs;

public class PurchaseDetailsDto:IPurchaseDetails
{
    public string PurchaseInvoiceId { get; set; }
    public string ProductId { get; set; }
    public decimal Amount { get; set; }
    public string SupplierId { get; set; }
}