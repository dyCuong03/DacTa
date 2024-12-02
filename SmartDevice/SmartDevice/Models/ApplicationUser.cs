using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace SmartDevice.Models;

public class ApplicationUser : IdentityUser
{
    [MaxLength(250)] public string? FullName { get; set; }
    [MaxLength(250)] public string? Birthday { get; set; }
    [MaxLength(250)] public string? Address { get; set; }
    [MaxLength(250)] public override string? PhoneNumber { get; set; }
    [MaxLength(250)] public string? Role { get; set; }

    public ICollection<SalesInvoice>? SalesInvoices { get; set; }
    public ICollection<PurchaseInvoice>? PurchaseInvoice { get; set; }
}