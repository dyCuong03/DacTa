using SmartDevice.Models.Interfaces;

namespace SmartDevice.DTOs;

public class ProductDto : IProduct
{
    public string? ProductId { get; set; }
    public required string ProductTypeId { get; set; }
    public required string BrandId { get; set; }
    public string? ProductName { get; set; }
    public decimal SalePrice { get; set; }
    public decimal Quantity { get; set; }
    public double? Weight { get; set; }
    public string? ColorOptions { get; set; }
    public int? WarrantyPeriod { get; set; }
    public decimal? PowerConsumption { get; set; }
    public string? Voltage { get; set; }
    public string? Connectivity { get; set; }
    public string? Features { get; set; }
    public IFormFile? Image { get; set; }
    public string? Description { get; set; }
}