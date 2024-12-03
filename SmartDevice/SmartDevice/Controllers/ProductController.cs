using Microsoft.AspNetCore.Mvc;
using SmartDevice.Datas;
using SmartDevice.DTOs;
using SmartDevice.Models;

namespace SmartDevice.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class ProductController:ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ProductController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    [HttpPost]
    public async Task<IActionResult> InsertProduct([FromForm] ProductDto productDto)
    {
        if (productDto.Image == null)
            return BadRequest("Image is required.");

        using var memoryStream = new MemoryStream();
        await productDto.Image.CopyToAsync(memoryStream);

        var product = new Product
        {
            ProductId = Guid.NewGuid()
                .ToString(),
            BrandId = productDto.BrandId,
            ProductTypeId = productDto.ProductTypeId,
            ProductName = productDto.ProductName,
            Description = productDto.Description,
            Image = memoryStream.ToArray(),
            SalePrice = productDto.SalePrice,
            Quantity = productDto.Quantity,
            Weight = productDto.Weight,
            ColorOptions = productDto.ColorOptions,
            WarrantyPeriod = productDto.WarrantyPeriod,
            PowerConsumption = productDto.PowerConsumption,
            Voltage = productDto.Voltage,
            Connectivity = productDto.Connectivity,
            Features = productDto.Features,
        };

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        return Ok(new { Message = "Product added successfully",  product.ProductId });
    }
}