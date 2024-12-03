using System.ComponentModel.DataAnnotations;

namespace SmartDevice.Models;

public class Supplier
{
    [Key] public required string SupplierId { get; set; }
    [MaxLength(250)] public string? SupplierName { get; set; }
    [MaxLength(250)] public string? Address { get; set; }
    [MaxLength(250)] public string? PhoneNumber { get; set; }
}