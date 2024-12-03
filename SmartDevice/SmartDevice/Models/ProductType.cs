using System.ComponentModel.DataAnnotations;

namespace SmartDevice.Models;

public class ProductType
{
    [Key]
    public string ProductTypeId { get; set; }
    [MaxLength(250)] public string? ProductTypeName { get; set; }
}