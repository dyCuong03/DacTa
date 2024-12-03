using System.ComponentModel.DataAnnotations;

namespace SmartDevice.Models;

public class ProductDetails
{
    public required string ProductId { get; set; }
    [MaxLength(250)]public string? SerialNumber { get; set; }
    [MaxLength(250)]public string? Status { get; set; }
    public Product? Product { get; set; }
}