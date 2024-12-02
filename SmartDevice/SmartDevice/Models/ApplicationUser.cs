using Microsoft.AspNetCore.Identity;

namespace SmartDevice.Models;

public class ApplicationUser : IdentityUser
{
    public string? FullName { get; set; }
    public string? Birthday { get; set; }
    public string? Address { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Role {get; set;}
    
    public ICollection<SalesInvoice>? SalesInvoices { get; set; }
    public ICollection<PurchaseInvoice>? PurchaseInvoice { get; set; }
}