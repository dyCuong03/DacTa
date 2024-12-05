using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartDevice.Datas;
using SmartDevice.DTOs;
using SmartDevice.Models;

namespace SmartDevice.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class BrandController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public BrandController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetBrands() 
        => Ok(await _context.Brand.ToListAsync());
}