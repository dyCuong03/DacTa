using System.ComponentModel.DataAnnotations;
using SmartDevice.Models.Interfaces;

namespace SmartDevice.Models;

public class ProductDetails : IProductDetails
{
    public required string ProductId { get; set; }
    [MaxLength(250)] public string? SerialNumber { get; set; }
    [MaxLength(250)] public string? Status { get; set; }
    public Product? Product { get; set; }
}