using Microsoft.AspNetCore.Mvc;
using SmartDevice.Datas;
using SmartDevice.DTOs;
using SmartDevice.Models;

namespace SmartDevice.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class SaleInvoiceController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public SaleInvoiceController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public Task<IActionResult> GetSaleInvoice(string salesInvoiceId) 
        =>
            Task.FromResult<IActionResult>(Ok( _context.SalesInvoices.Where(o => o.SalesInvoiceId == salesInvoiceId)));

    [HttpPost]
    public async Task<IActionResult> AddSaleInvoice([FromBody] SaleInvoiceDto salesInvoiceDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var salesInvoice = new SalesInvoice
            {
                SalesInvoiceId = Guid.NewGuid().ToString(),
                InvoiceDate = salesInvoiceDto.InvoiceDate,
                TotalAmount = salesInvoiceDto.TotalAmount,
                EmployeeId = salesInvoiceDto.EmployeeId,
                CustomerId = salesInvoiceDto.CustomerId
            };

            _context.SalesInvoices.Add(salesInvoice);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSaleInvoiceById), new { id = salesInvoice.SalesInvoiceId }, salesInvoice);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while adding the sale invoice.", error = ex.Message });
        }
        
        
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetSaleInvoiceById(string id)
    {
        var salesInvoice = await _context.SalesInvoices.FindAsync(id);
        if (salesInvoice == null)
        {
            return NotFound(new { message = "Sale invoice not found." });
        }
        return Ok(salesInvoice);
    }

}