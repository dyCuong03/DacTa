using SmartDevice.Models;
using SmartDevice.Models.Interfaces;

namespace SmartDevice.DTOs;

public class ProductDetailsDto : IProductDetails
{
    public string ProductId { get; set; }
    public string? SerialNumber { get; set; }
    public string? Status { get; set; }
    public Product? Product { get; set; }
}