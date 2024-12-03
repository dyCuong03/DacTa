using System.ComponentModel.DataAnnotations;

namespace SmartDevice.Models;

public class SalesInvoice
{
    [Key]
    public required string SalesInvoiceId { get; set; }
    public DateTime InvoiceDate { get; set; }
    public decimal TotalAmount { get; set; }
    public required string EmployeeId { get; set; }
    public required string CustomerId { get; set; }
    public Customer? Customer { get; set; }
    public ApplicationUser? ApplicationUser { get; set; }
    public ICollection<SalesDetails>? SalesDetails { get; set; }
}