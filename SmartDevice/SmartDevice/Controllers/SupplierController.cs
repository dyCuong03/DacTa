using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartDevice.Datas;

namespace SmartDevice.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class SupplierController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public SupplierController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetSupplier() => Ok(await _context.Suppliers.ToListAsync());
}