using System.ComponentModel.DataAnnotations;

namespace SmartDevice.Models;

public class Product
{
    [Key]
    public required int ProductId { get; set; }
    public required int ProductTypeId { get; set; }
    public required int BrandId { get; set; }
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
    public string? Image { get; set; }
    public string? Description { get; set; }
    public ProductType? ProductType { get; set; }
    public Brand? Brand { get; set; }
    public ICollection<ProductDetails>? ProductDetails { get; set; }
}