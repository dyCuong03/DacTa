using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartDevice.Datas;
using SmartDevice.DTOs;
using SmartDevice.Models;

namespace SmartDevice.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public class ProductDetailsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ProductDetailsController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetProductDetails() 
        => Ok(await _context.ProductDetails.ToListAsync());
    
    [HttpPost]
    public async Task<IActionResult> InsertProductDetails([FromForm] ProductDetailsDto productDetailsDto)
    {
        var productDetails = new ProductDetails
        {
            ProductId = productDetailsDto.ProductId,
            SerialNumber = productDetailsDto.SerialNumber,
            Status = productDetailsDto.Status,
        };

        _context.ProductDetails.Add(productDetails);
        await _context.SaveChangesAsync();

        return Ok(new { Message = "ProductDetails added successfully", productDetailsDto.ProductId });
    }
}