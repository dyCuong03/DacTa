using System.ComponentModel.DataAnnotations;

namespace SmartDevice.Models;

public class Customer
{
    [Key]
    public required int CustomerId { get; set; }
    public string? CustomerName { get; set; }
    public string? Address { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
}