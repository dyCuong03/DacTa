using System.ComponentModel.DataAnnotations;

namespace SmartDevice.Models;

public class Customer
{
    [Key] public required string CustomerId { get; set; }
    [MaxLength(250)] public string? CustomerName { get; set; }
    [MaxLength(250)] public string? Address { get; set; }
    [MaxLength(250)] public string? PhoneNumber { get; set; }
    [MaxLength(250)] public string? Email { get; set; }
}