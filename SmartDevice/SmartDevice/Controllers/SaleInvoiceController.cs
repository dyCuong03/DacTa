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
    public async Task<IActionResult> AddSaleInvoice( SaleInvoice salesInvoiceDto)
    {
            var salesInvoice = new SalesInvoice
            {
                SalesInvoiceId = Guid.NewGuid().ToString(),
                InvoiceDate = DateTime.Now,
                TotalAmount = 0,
                EmployeeId = salesInvoiceDto.EmployeeId,
                CustomerId = salesInvoiceDto.CustomerId
            };

            _context.SalesInvoices.Add(salesInvoice);
            await _context.SaveChangesAsync();
            return Ok(salesInvoice);
         
        
        
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
    
    [HttpDelete("{salesInvoiceId}")]
    public async Task<IActionResult> DeleteSaleInvoice(string salesInvoiceId)
    {
        try
        {
            var salesInvoice = await _context.SalesInvoices.FindAsync(salesInvoiceId);
        
            if (salesInvoice == null)
            {
                return NotFound(new { message = "Sale invoice not found." });
            }

            _context.SalesInvoices.Remove(salesInvoice);
        
            await _context.SaveChangesAsync();

            return Ok(new { message = "Sale invoice deleted successfully." });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while deleting the sale invoice.", error = ex.Message });
        }
    }

}