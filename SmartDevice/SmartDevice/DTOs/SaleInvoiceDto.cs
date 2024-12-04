using SmartDevice.Models;
using SmartDevice.Models.Interfaces;

namespace SmartDevice.DTOs;

public class SaleInvoiceDto : ISaleInvoice
{
    public string SalesInvoiceId { get; set; }
    public DateTime InvoiceDate { get; set; }
    public decimal TotalAmount { get; set; }
    public string? EmployeeId { get; set; }
    public string? CustomerId { get; set; }
}