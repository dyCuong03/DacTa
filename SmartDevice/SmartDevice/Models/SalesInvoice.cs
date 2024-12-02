using System.ComponentModel.DataAnnotations;

namespace SmartDevice.Models;

public class SalesInvoice
{
    [Key]
    public required int SalesInvoiceId { get; set; }
    public DateTime InvoiceDate { get; set; }
    public decimal TotalAmount { get; set; }
    public required int EmployeeId { get; set; }
    public required int CustomerId { get; set; }
    public Customer? Customer { get; set; }
    public ApplicationUser? ApplicationUser { get; set; }
    
    public ICollection<SalesDetails>? SalesDetails { get; set; }
}