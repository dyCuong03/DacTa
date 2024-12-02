namespace SmartDevice.Models;

public class ProductDetails
{
    public required int ProductId { get; set; }
    public string? SerialNumber { get; set; }
    public string? Status { get; set; }
    public Product? Product { get; set; }
}