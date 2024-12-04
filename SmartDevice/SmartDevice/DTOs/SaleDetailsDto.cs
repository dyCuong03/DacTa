using SmartDevice.Models.Interfaces;

namespace SmartDevice.DTOs;

public class SaleDetailsDto : ISaleDetails
{
    public string SalesInvoiceId { get; set; }
    public string ProductId { get; set; }
    public decimal Amount { get; set; }
}