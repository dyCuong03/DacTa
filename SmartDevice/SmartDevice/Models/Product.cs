using System.ComponentModel.DataAnnotations;
using SmartDevice.Models.Interfaces;

namespace SmartDevice.Models;

public class Product : IProduct
{
    [Key] [MaxLength(250)] public required string ProductId { get; set; }
    [MaxLength(250)] public required string ProductTypeId { get; set; }
    [MaxLength(250)] public required string BrandId { get; set; }
    [MaxLength(250)] public string? ProductName { get; set; }
    public decimal SalePrice { get; set; }
    public decimal Quantity { get; set; }
    public double? Weight { get; set; }
    [MaxLength(250)] public string? ColorOptions { get; set; }
    public int? WarrantyPeriod { get; set; }
    public decimal? PowerConsumption { get; set; }
    [MaxLength(250)] public string? Voltage { get; set; }
    [MaxLength(250)] public string? Connectivity { get; set; }
    [MaxLength(250)] public string? Features { get; set; }
    public byte[]? Image { get; set; }
    [MaxLength(250)] public string? Description { get; set; }
    public ProductType? ProductType { get; set; }
    public Brand? Brand { get; set; }
    public ICollection<ProductDetails>? ProductDetails { get; set; }
}