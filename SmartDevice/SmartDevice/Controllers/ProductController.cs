using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartDevice.Datas;
using SmartDevice.DTOs;
using SmartDevice.Models;
using SmartDevice.Service;

namespace SmartDevice.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IImageService _imageService;

    public ProductController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts()
        => Ok(await _context.Products
            .Include(c => c.Brand)
            .ToListAsync());
    
    [HttpPost]
    public async Task<IActionResult> InsertProduct([FromForm] ProductDto productDto)
    {
        if (productDto == null) return BadRequest("foodimagDTo is null.");
        string imgPath = null;
        if (productDto.Image != null)
        {
                
            var uploadResult = await _imageService.AddImageAsync(productDto.Image);
            if (uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
            {
                imgPath = uploadResult.SecureUrl.AbsoluteUri;
            }
            else
            {
                return StatusCode((int)uploadResult.StatusCode, "Image upload failed.");
            }
        }
        var product = new Product
        {
            ProductId = Guid.NewGuid()
                .ToString(),
            BrandId = productDto.BrandId,
            ProductTypeId = productDto.ProductTypeId,
            ProductName = productDto.ProductName,
            Description = productDto.Description,
            Image = imgPath,
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

        return Ok(new { Message = "Product added successfully", product.ProductId });
    }
    
    
    
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] FormFile Img)
    {
        string imgPath = null;
        if (Img != null)
        {
                
            var uploadResult = await _imageService.AddImageAsync(Img);
            if (uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
            {
                imgPath = uploadResult.SecureUrl.AbsoluteUri;
            }
            else
            {
                return StatusCode((int)uploadResult.StatusCode, "Image upload failed.");
            }
        }
     
        return Ok(new { Message = "Product added successfully", imgPath });
    }
}