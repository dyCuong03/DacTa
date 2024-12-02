using System.ComponentModel.DataAnnotations;

namespace SmartDevice.Models;

public class Supplier
{
    [Key]
    public required int SupplierId { get; set; }
    public string? SupplierName { get; set; }
    public string? Address { get; set; }
    public string? PhoneNumber { get; set; }
}