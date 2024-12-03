using System.ComponentModel.DataAnnotations;

namespace SmartDevice.Models;

public class Brand
{
    [Key] public required string BrandId { get; set; }
    [MaxLength(250)] public string? BrandName { get; set; }
}