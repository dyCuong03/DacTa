namespace SmartDevice.Models.Interfaces;

public interface IProductDetails
{
    public string ProductId { get; set; }
    public string? SerialNumber { get; set; }
    public string? Status { get; set; }
    public Product? Product { get; set; }
}